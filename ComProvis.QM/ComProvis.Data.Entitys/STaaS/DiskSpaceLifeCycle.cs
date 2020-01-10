using System;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskSpaceLifeCycle
    {
        public int Id { get; set; }
        public int DiskSpaceId { get; set; }
        public byte DiskSpaceStateId { get; set; }
        public int ProductId { get; set; }
        public bool IsProductChange { get; set; }
        public bool IsUpgrade { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LogTime { get; set; }

        public DiskSpace DiskSpace { get; set; }
        public DiskSpaceState DiskSpaceState { get; set; }
        public Products Product { get; set; }
    }
}
