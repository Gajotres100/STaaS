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
    [Provision(ProvisionType.UpgradeDiskSpace)]
    public class UpgradeDiskSpace : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IDiskSpaceRepository _diskSpaceRepository;
        IUserRepository _userRepository;

        public UpgradeDiskSpace(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IDiskSpaceRepository diskSpaceRepository, IUserRepository userRepository) : base(logRepository)
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
                UpgradeDiskSpaceData json = JsonConvert.DeserializeObject<UpgradeDiskSpaceData>(data);
                operationGuid = json.OrderDemandGuid;

                var diskSpace = _diskSpaceRepository.GetDiskSpaceInfo(json.UserId, json.DiskSpaceId, true);
                var product = _diskSpaceRepository.GetProductsById(json.ProductId);
                _diskSpaceRepository.UpdateDiskSpace(diskSpace.DiskSpaceID, null, json.ProductId);

                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);

                SendNotification(json, product.Name, diskSpace.DiskSpaceName);
            }

            catch (Exception ex)
            {
                _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
                _logRepository.InsertLogoRecord(nameof(CreateDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            }
        }

        void SendNotification(UpgradeDiskSpaceData json, string diskSpacePlan, string diskSpaceName)
        {
            var operationGuid = Guid.NewGuid().ToString();
            try
            {
                var user = _userRepository.GetUserById(json.UserId);
                var upgradeDiskSpaceMailObject = new UpgradeDiskSpaceMailObject
                {
                    TemplateName = nameof(MailTemplateName.STaaSUpgradeDiskSpace),
                    OrderDemandGuid = operationGuid,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DiskSpaceName = diskSpaceName,
                    DiskSpacePlan = diskSpacePlan
                };
                _orderDemandRepository.SaveOrderDemand(null, operationGuid, json.CompanyId, (int)ProvisionType.SendMail, (int)OrderDemandStates.Created, (int)OrderDemandType.Integrated, JsonConvert.SerializeObject(upgradeDiskSpaceMailObject), null);
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(SendNotification) + " " + nameof(DeleteDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, null);
            }
        }
    }
}
