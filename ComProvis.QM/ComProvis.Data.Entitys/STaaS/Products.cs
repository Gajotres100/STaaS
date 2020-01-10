using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComProvis.Data.Entitys.STaaS
{
    [Table(nameof(Products), Schema = "STaaS.Codebook")]
    public class Products
    {
        public Products()
        {
            DiskSpaceLifeCycle = new HashSet<DiskSpaceLifeCycle>();
            ProductsHistory = new HashSet<ProductsHistory>();
        }

        public int ProductId { get; set; }
        public int DiskSize { get; set; }
        public byte DiskSizeUnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumOfUsers { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public DiskSizeUnits DiskSizeUnit { get; set; }
        public ICollection<DiskSpaceLifeCycle> DiskSpaceLifeCycle { get; set; }
        public ICollection<ProductsHistory> ProductsHistory { get; set; }
    }
}
