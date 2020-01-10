using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ComProvis.Data.Entitys.STaaS
{
    [Table(nameof(DiskSpace), Schema = "STaaS")]
    public class DiskSpace
    {
        public DiskSpace()
        {
            DiskSpaceLifeCycle = new HashSet<DiskSpaceLifeCycle>();
            DiskSpaceUsers = new HashSet<DiskSpaceUsers>();
            DiskUnitShareGroup = new HashSet<DiskUnitShareGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiskSpaceID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int DiskSpaceStateID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid Identifier { get; set; }

        [Required]
        public int OwnerID { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }


        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<DiskSpaceLifeCycle> DiskSpaceLifeCycle { get; set; }
        public ICollection<DiskSpaceUsers> DiskSpaceUsers { get; set; }
        public ICollection<DiskUnitShareGroup> DiskUnitShareGroup { get; set; }

    }
}
