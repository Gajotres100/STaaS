using System.Collections.Generic;

namespace ComProvis.Data.Entitys.STaaS
{
    public class DiskSpaceState
    {
        public DiskSpaceState()
        {
            DiskSpaceLifeCycle = new HashSet<DiskSpaceLifeCycle>();
        }

        public byte DiskSpaceStateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<DiskSpaceLifeCycle> DiskSpaceLifeCycle { get; set; }
    }
}
