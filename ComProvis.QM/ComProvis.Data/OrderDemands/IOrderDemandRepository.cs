using ComProvis.Data.Entitys.QM;
using System.Collections.Generic;

namespace ComProvis.Data.DemandOrder
{
    public interface IOrderDemandRepository
    {
        List<SpGetOrderDemandsForProvisioning> GetOrderDemand();
        List<SpGetOrderDemandsByProvisninType> GetOrderDemandsByProvisninType(int provisioningTypeId);
        void SaveOrderDemand(int? parentDemandId, string guid, int? mkpCustomerId, int? mkpOrderId, int? provisioningTypeId, int? orderDemandStateId, int? orderDemandTypeId, string jsondata);
        SpGetOrderDemandByGuid GetOrderDemandByGuid(string orderDemandGuid);
        void ChangeOrderDemandState(string orderDemandGuid, int? orderDemandStateId);
        void SaveOrderDemand(int? parentDemandId, string guid, int? companyId, int? provisioningTypeId, int? orderDemandStateId, int? orderDemandTypeId, string jsondata, string externalTransactionId);
    }
}