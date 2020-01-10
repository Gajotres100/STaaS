using System;
using System.Collections.Generic;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskUnits
    {
        public DiskUnits()
        {
            DiskUnitRolePermission = new HashSet<DiskUnitRolePermission>();
            DiskUnitShare = new HashSet<DiskUnitShare>();
            DiskUnitUserPermission = new HashSet<DiskUnitUserPermission>();
            InverseDiskUnitParent = new HashSet<DiskUnits>();
        }

        public int DiskUnitId { get; set; }
        public int? DiskUnitParentId { get; set; }
        public byte DiskUnitTypeId { get; set; }
        public int DiskSpaceId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public Guid Identifier { get; set; }
        public int? OwnerId { get; set; }
        public DateTime CreateDate { get; set; }

        public DiskUnits DiskUnitParent { get; set; }
        public DiskUnitType DiskUnitType { get; set; }
        public ICollection<DiskUnitRolePermission> DiskUnitRolePermission { get; set; }
        public ICollection<DiskUnitShare> DiskUnitShare { get; set; }
        public ICollection<DiskUnitUserPermission> DiskUnitUserPermission { get; set; }
        public ICollection<DiskUnits> InverseDiskUnitParent { get; set; }
    }
}
