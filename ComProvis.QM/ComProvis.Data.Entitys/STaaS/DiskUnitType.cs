using System.Collections.Generic;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskUnitType
    {
        public DiskUnitType()
        {
            DiskUnits = new HashSet<DiskUnits>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<DiskUnits> DiskUnits { get; set; }
    }
}
