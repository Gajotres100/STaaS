using ComProvis.Data.Entitys.STaaS;
using System;
using System.Collections.Generic;

namespace ComProvis.Data.Disks
{
    public interface IDiskSpaceRepository
    {
        void CreateDiskSpace(int? productId, int? userId, byte? diskSpaceStateId, string diskSpaceName, string diskSpaceDescription, Guid? identifier, Guid? assetGroupId);

        SpGetDiskSpaceInfo GetDiskSpaceInfo(int? userId, int? diskSpaceId, bool? onlyActive);

        void DeleteDiskSpace(int? diskSpaceID, int? userID);

        void UpdateDiskSpace(int? diskSpaceID, string name, int? productID);

        List<DiskSpace> GetCustomersDiskSpace(string externalId);

        Products GetProductsById(int productId);
    }
}