using ComProvis.Data.Entitys.QM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComProvis.Data.DemandOrder
{
    public class OrderDemandRepository : IOrderDemandRepository
    {
        #region Properties

        private readonly IConfiguration _configuration;

        public QmContext Context => new QmContext(_configuration);

        #endregion

        #region Constructor

        public OrderDemandRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public List<SpGetOrderDemandsForProvisioning> GetOrderDemand()
        {
            using (var context = Context)
            {
                return context.SpGetOrderDemandsForProvisionings.FromSql("EXECUTE [QM].[GetOrderDemandsForProvisioning]").ToList();
            }
        }


        public List<SpGetOrderDemandsByProvisninType> GetOrderDemandsByProvisninType(int provisioningTypeId)
        {
            using (var context = Context)
            {
                return context.SpGetOrderDemandsByProvisninTypes.FromSql("EXECUTE [QM].[GetOrderDemandsByProvisninType] @p0", provisioningTypeId).ToList();
            }
        }
        public void SaveOrderDemand(int? parentDemandId, string guid, int? mkpCustomerId, int? mkpOrderId, int? provisioningTypeId, int? orderDemandStateId, int? orderDemandTypeId, string jsondata)
        {
            throw new NotImplementedException();
        }

        public SpGetOrderDemandByGuid GetOrderDemandByGuid(string orderDemandGuid)
        {
            using (var context = Context)
            {
                return context.SpGetOrderDemandByGuids.FromSql("EXECUTE [QM].[GetOrderDemandByGuid] @p0", orderDemandGuid).FirstOrDefault();
            }
        }

        public void ChangeOrderDemandState(string orderDemandGuid, int? orderDemandStateId)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [QM].[ChangeOrderDemandState] @p0, @p1", orderDemandGuid, orderDemandStateId);
            }
        }

        public void SaveOrderDemand(int? parentDemandId, string guid, int? companyId, int? provisioningTypeId, int? orderDemandStateId, int? orderDemandTypeId, string jsondata, string externalTransactionId)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [QM].[SaveOrderDemand] @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7", parentDemandId, guid, companyId, provisioningTypeId, orderDemandStateId, orderDemandTypeId, jsondata, externalTransactionId);
            }            
        }

        #endregion
    }
}
