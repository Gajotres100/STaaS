using System;

namespace ComProvis.Data.Entitys.STaaS
{
    public class ProductsHistory
    {
        public int RowId { get; set; }
        public int ProductId { get; set; }
        public int DiskSize { get; set; }
        public byte DiskSizeUnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumOfUsers { get; set; }
        public bool IsLocked { get; set; }
        public string Action { get; set; }
        public DateTime DateModified { get; set; }

        public Products Product { get; set; }
    }
}
