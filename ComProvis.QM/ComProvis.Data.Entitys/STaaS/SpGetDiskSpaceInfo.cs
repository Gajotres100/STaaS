using System;
using System.ComponentModel.DataAnnotations;

namespace ComProvis.Data.Entitys.STaaS
{
    public class SpGetDiskSpaceInfo
    {     
        [Key]
        public int DiskSpaceID { get; set; }

        public string DiskSpaceName { get; set; }

        public string Description { get; set; }

        public byte DiskSpaceStateID { get; set; }

        public Guid Identifier { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string DiskSpaceStateName { get; set; }

        public long? DiskSizeInBytes { get; set; }

        public long? BytesUsed { get; set; }
    }
}
