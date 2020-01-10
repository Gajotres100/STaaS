using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComProvis.Data.Entitys.QM
{
    public class OrderDemand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentDemandId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Guid { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int ProvisioningTypeId { get; set; }
        [Required]
        public int OrderDemandStateId { get; set; }
        [Required]
        public int OrderDemandTypeId { get; set; }
        [Required]
        public string JsonData { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
