using System.Linq;

namespace ComProvis.Api.Data.Customers
{
    public class CustomerManager
    {
        #region Properties

        public STaaSContext Context => new STaaSContext();

        #endregion

        #region Constructor

        public CustomerManager()
        {

        }

        #endregion

        #region Methods

        public SpGetCompanyByExternalId GetCompanyByExternalId(string externalId)
        {
            using (var context = Context)
            {
                return context.GetCompanyByExternalId(externalId).FirstOrDefault();
            }
        }

        public SpGetUserByExternalId GetUserByExternalId(string externalId)
        {
            using (var context = Context)
            {
                return context.GetUserByExternalId(externalId).FirstOrDefault();
            }
        }

        public SpGetCompanyFirstAdminByExternalId GetCompanyFirstAdminByExternalId(string externalId)
        {
            using (var context = Context)
            {
                return context.GetCompanyFirstAdminByExternalId(externalId).FirstOrDefault();
            }
        }

        public Products GetProductByProductExternalId(string externalId)
        {
            using (var context = Context)
            {
                return context.Products.FirstOrDefault(p => p.ExternalProductId.Equals(externalId));
            }
        }

        public DiskSpace GetDiskSpaceByAssetGroupId(string assetGroupId)
        {
            using (var context = Context)
            {
                return context.DiskSpace.FirstOrDefault(d => d.AsetGroupID.Equals(assetGroupId));
            }
        }

        public Roles GetRolesByExternalId(string externalId)
        {
            using (var context = Context)
            {
                return context.Roles.FirstOrDefault(d => d.ExternalId.Equals(externalId));
            }
        }


        #endregion
    }
}
