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
    [Provision(ProvisionType.DeleteDiskSpace)]
    public class DeleteDiskSpace : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        ISTaaSSoap _sTaaSSoap;
        IDiskSpaceRepository _diskSpaceRepository;
        IUserRepository _userRepository;

        public DeleteDiskSpace(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, ISTaaSSoap sTaaSSoap, IDiskSpaceRepository diskSpaceRepository, IUserRepository userRepository) : base(logRepository)
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
                DeleteDiskSpaceData json = JsonConvert.DeserializeObject<DeleteDiskSpaceData>(data);
                operationGuid = json.OrderDemandGuid;

                var diskSpace = _diskSpaceRepository.GetDiskSpaceInfo(json.UserId, json.DiskSpaceId, true);
                _sTaaSSoap.RenamePhysicalFolder(diskSpace.Identifier.ToString("N"));
                _diskSpaceRepository.DeleteDiskSpace(diskSpace.DiskSpaceID, json.UserId);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);

                SendNotification(json, diskSpace.DiskSpaceName);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(DeleteDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }

        void SendNotification(DeleteDiskSpaceData json, string diskName)
        {
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                var user = _userRepository.GetUserById(json.UserId);
                var deleteDiskSpaceMailObject = new DeleteDiskSpaceMailObject
                {
                    TemplateName = nameof(MailTemplateName.STaaSDeleteDiskSpace),
                    OrderDemandGuid = operationGuid,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DiskSpaceName = diskName
                };
                _orderDemandRepository.SaveOrderDemand(null, operationGuid, json.CompanyId, (int)ProvisionType.SendMail, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(deleteDiskSpaceMailObject), null);
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(SendNotification) + " " + nameof(DeleteDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, null);
            }
        }
    }
}
