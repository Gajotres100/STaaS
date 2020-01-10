using System.Linq;

namespace ComProvis.Api.Data.OrderDemands
{
    public class OrderDemandManager
    {
        #region Properties

        public STaaSContext Context => new STaaSContext();

        #endregion

        #region Constructor

        public OrderDemandManager()
        {

        }

        #endregion

        #region Methods

        public SpGetOrderDemandByGuid GetOrderDemandByGuid(string orderDemandGuid)
        {
            using (var context = Context)
            {
                return context.GetOrderDemandByGuid(orderDemandGuid).FirstOrDefault();
            }
        }

        public SpGetOrderDemandByExternalTransactionId GetOrderDemandByExternalTransactionId(string orderDemandGuid)
        {
            using (var context = Context)
            {
                return context.GetOrderDemandByExternalTransactionId(orderDemandGuid).FirstOrDefault();
            }
        }

        public void SaveOrderDemand(int? parentDemandId, string guid, int? companyId, int? provisioningTypeId, int? orderDemandStateId, int? orderDemandTypeId, string jsondata, string externalTransactionId)
        {
            using (var context = Context)
            {
                context.SaveOrderDemand(parentDemandId, guid, companyId, provisioningTypeId, orderDemandStateId, orderDemandTypeId, jsondata, externalTransactionId);
            }
        }

        #endregion
    }
}
