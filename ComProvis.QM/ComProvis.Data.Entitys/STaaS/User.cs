using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComProvis.Data.Entitys.STaaS
{
    [Table(nameof(User), Schema = "STaaS")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(36)]
        public string ExternalId { get; set; }

        public int? CompanyId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        public int? RoleId { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string ContactInfo { get; set; }

        public bool IsSuspended { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public Company Company { get; set; }
    }
}
