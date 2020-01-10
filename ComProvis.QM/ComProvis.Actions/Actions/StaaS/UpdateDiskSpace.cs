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
    [Provision(ProvisionType.UpdateDiskSpace)]
    public class UpdateDiskSpace : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IDiskSpaceRepository _diskSpaceRepository;
        IUserRepository _userRepository;

        public UpdateDiskSpace(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IDiskSpaceRepository diskSpaceRepository, IUserRepository userRepository) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _diskSpaceRepository = diskSpaceRepository;
            _userRepository = userRepository;
        }

        public override void DoJob(dynamic data)
        {
            var operationGuid = string.Empty;

            try
            {
                UpdateDiskSpaceData json = JsonConvert.DeserializeObject<UpdateDiskSpaceData>(data);
                operationGuid = json.OrderDemandGuid;

                var diskSpace = _diskSpaceRepository.GetDiskSpaceInfo(json.UserId, json.DiskSpaceId, true);
                _diskSpaceRepository.UpdateDiskSpace(diskSpace.DiskSpaceID, json.DiskSpaceName, null);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);

                SendNotification(json);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(CreateDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }

        void SendNotification(UpdateDiskSpaceData json)
        {
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                var user = _userRepository.GetUserById(json.UserId);
                var updateDiskSpaceMailObject = new UpdateDiskSpaceMailObject
                {
                    TemplateName = nameof(MailTemplateName.STaaSUpdateDiskSpace),
                    OrderDemandGuid = operationGuid,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DiskSpaceName = json.DiskSpaceName
                };
                _orderDemandRepository.SaveOrderDemand(null, operationGuid, json.CompanyId, (int)ProvisionType.SendMail, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(updateDiskSpaceMailObject), null);
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(SendNotification) + " " + nameof(DeleteDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, null);
            }
        }
    }
}
