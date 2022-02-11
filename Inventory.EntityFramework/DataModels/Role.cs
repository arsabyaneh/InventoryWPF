namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Role")]
    public partial class Role : BaseDataModel
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
