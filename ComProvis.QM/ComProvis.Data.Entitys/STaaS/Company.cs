using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComProvis.Data.Entitys.STaaS
{
    [Table(nameof(Company), Schema = "STaaS")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(36)]
        public string ExternalId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string ContactEmail { get; set; }

        [MaxLength(255)]
        public string ContactFirstName { get; set; }

        [MaxLength(255)]
        public string ContactLastName { get; set; }

        public bool IsSuspended { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }


        public List<User> Users { get; set; }

        public List<DiskSpace> DiskSpaces { get; set; }
    }
}
