using ComProvis.Actions.Actions.STaaSProxy;
using ComProvis.Actions.Helper;
using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Disks;
using ComProvis.Data.Log;
using ComProvis.Enums;
using ComProvis.Params.Data.STaaSData;
using ComProvis.Params.MailObjects;
using Newtonsoft.Json;
using System;

namespace ComProvis.Actions.Actions.StaaS
{
    [Provision(ProvisionType.CreateDiskSpace)]
    public class CreateDiskSpace : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        ISTaaSSoap _sTaaSSoap;
        IDiskSpaceRepository _diskSpaceRepository;
        IUserRepository _userRepository;

        public CreateDiskSpace(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, ISTaaSSoap sTaaSSoap, IDiskSpaceRepository diskSpaceRepository, IUserRepository userRepository) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _sTaaSSoap = sTaaSSoap;
            _diskSpaceRepository = diskSpaceRepository;
            _userRepository = userRepository;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                CreateDiskSpaceData json = JsonConvert.DeserializeObject<CreateDiskSpaceData>(data);
                operationGuid = json.OrderDemandGuid;

                var folderGuid = new Guid(operationGuid);
                _sTaaSSoap.DiskSpaceCreate(folderGuid.ToString("N"));
                _diskSpaceRepository.CreateDiskSpace(json.ProductId, json.UserId, (byte?)DiskSpaceState.Active, json.DiskName, json.Description, folderGuid, new Guid(json.AssetGroupId));                

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);

                SendNotification(json);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(CreateDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }

        void SendNotification(CreateDiskSpaceData json)
        {            
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                var user = _userRepository.GetUserById(json.UserId);
                var createDiskSpaceMailObject = new CreateDiskSpaceMailObject
                {
                    TemplateName = nameof(MailTemplateName.STaaSCreateDiskSpace),
                    OrderDemandGuid = operationGuid,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DiskSpaceName = json.DiskName
                };
                _orderDemandRepository.SaveOrderDemand(null, operationGuid, json.CompanyId, (int)ProvisionType.SendMail, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(createDiskSpaceMailObject), null);
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(SendNotification) + " " + nameof(CreateDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, null);                
            }
        }
    }
}
