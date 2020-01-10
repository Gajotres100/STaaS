using ComProvis.Data.Entitys.STaaS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComProvis.Data.Disks
{
    public class DiskSpaceRepository : IDiskSpaceRepository
    {
        #region Properties

        private readonly IConfiguration _configuration;

        public STaaSContext Context => new STaaSContext(_configuration);

        #endregion

        #region Constructor

        public DiskSpaceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public void CreateDiskSpace(int? productId, int? userId, byte? diskSpaceStateId, string diskSpaceName, string diskSpaceDescription, Guid? identifier, Guid? assetGroupId)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [STaaS].[CreateDiskSpace] @p0, @p1, @p2, @p3, @p4, @p5, @p6", productId, userId, diskSpaceStateId, diskSpaceName, diskSpaceDescription, identifier, assetGroupId);
            }
        }


        public void DeleteDiskSpace(int? diskSpaceID, int? userId)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [STaaS].[DeleteDiskSpace] @p0, @p1", diskSpaceID, userId);
            }
        }

        public SpGetDiskSpaceInfo GetDiskSpaceInfo(int? userId, int? diskSpaceId, bool? onlyActive)
        {
            using (var context = Context)
            {
                return context.SpGetDiskSpaceInfos.FromSql("EXECUTE [STaaS].[GetDiskSpaceInfo] @p0, @p1, @p2", userId, diskSpaceId, onlyActive).FirstOrDefault();
            }
        }

        public void UpdateDiskSpace(int? diskSpaceID, string name, int? productID)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [STaaS].[UpdateDiskSpace] @p0, @p1, @p2", diskSpaceID, name, productID);
            }
        }               

        public List<DiskSpace> GetCustomersDiskSpace(string externalId)
        {
            using (var context = Context)
            {
                return context.DiskSpaces.Where(d => d.Company.ExternalId == externalId && (d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Active || d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Suspended)).ToList();
            }
        }    
        
        public Products GetProductsById(int productId)
        {
            using (var context = Context)
            {
                return context.Productses.FirstOrDefault(p => p.ProductId == productId);
            }
        }

        #endregion
    }
}
