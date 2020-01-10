using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComProvis.Data.Entitys.STaaS
{
    [Table(nameof(Roles), Schema = "STaaS.Codebook")]
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDRole { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(450)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(36)]
        public string ExternalId { get; set; }

    }
}
