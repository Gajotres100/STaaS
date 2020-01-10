using System;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskSpaceUsers
    {
        public int Id { get; set; }
        public int DiskSpaceId { get; set; }
        public int UserId { get; set; }
        public bool IsOwner { get; set; }
        public bool CanAccess { get; set; }
        public DateTime CreateDate { get; set; }

        public DiskSpace DiskSpace { get; set; }
    }
}
