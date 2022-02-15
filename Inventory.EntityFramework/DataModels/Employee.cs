namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee : BaseDataModel
    {
        public Employee()
        {
            Invoices = new HashSet<Invoice>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
