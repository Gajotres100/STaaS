using System;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskUnitShare
    {
        public int Id { get; set; }
        public int DiskUnitId { get; set; }
        public int ShareGroupId { get; set; }
        public DateTime CreateDate { get; set; }

        public DiskUnits DiskUnit { get; set; }
        public DiskUnitShareGroup ShareGroup { get; set; }
    }
}
