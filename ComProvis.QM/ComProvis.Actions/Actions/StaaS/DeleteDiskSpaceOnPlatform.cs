using ComProvis.Actions.Helper;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Disks;
using ComProvis.Data.Log;
using ComProvis.Enums;

namespace ComProvis.Actions.Actions.StaaS
{
    [Provision(ProvisionType.DeleteDiskSpaceOnPlatform)]
    public class DeleteDiskSpaceOnPlatform : Job
    {
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;
        IDiskSpaceRepository _diskSpaceRepository;

        public DeleteDiskSpaceOnPlatform(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository, IDiskSpaceRepository diskSpaceRepository) : base(logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
            _diskSpaceRepository = diskSpaceRepository;
        }

        public override void DoJob(dynamic data)
        {
            //var operationGuid = string.Empty;

            //try
            //{
            //    //UpdateDiskSpaceData json = JsonConvert.DeserializeObject<UpdateDiskSpaceData>(data);
            //    operationGuid = json.OrderDemandGuid;

            //    var diskSpace = _diskSpaceRepository.GetDiskSpaceInfo(json.UserId, json.DiskSpaceId, true);
            //    _diskSpaceRepository.UpdateDiskSpace(diskSpace.DiskSpaceID, json.DiskSpaceName, null);

            //    _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.Finished);
            //}

            //catch (Exception ex)
            //{
            //    _orderDemandRepository.ChangeOrderDemandState(operationGuid, (int)OrderDemandStates.FinishedError);
            //    _logRepository.InsertLogoRecord(nameof(CreateDiskSpace), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, operationGuid, data);
            //}
        }
    }
}
