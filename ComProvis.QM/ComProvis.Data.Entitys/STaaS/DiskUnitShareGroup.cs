using System;
using System.Collections.Generic;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskUnitShareGroup
    {
        public DiskUnitShareGroup()
        {
            DiskUnitShare = new HashSet<DiskUnitShare>();
        }

        public int ShareGroupId { get; set; }
        public int DiskSpaceId { get; set; }
        public Guid ShareGuid { get; set; }
        public string ShortIdentifier { get; set; }
        public string ShareName { get; set; }
        public string ShareWith { get; set; }
        public string SharePassword { get; set; }
        public int SharedByUserId { get; set; }
        public string MsgText { get; set; }
        public bool? PreviewContent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public DiskSpace DiskSpace { get; set; }
        public ICollection<DiskUnitShare> DiskUnitShare { get; set; }
    }
}
