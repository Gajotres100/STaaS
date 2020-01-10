using System;
using System.Collections.Generic;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskSizeUnits
    {
        public DiskSizeUnits()
        {
            Products = new HashSet<Products>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public long Koef { get; set; }
        public bool IsBasic { get; set; }
        public bool CanUseForProducts { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
