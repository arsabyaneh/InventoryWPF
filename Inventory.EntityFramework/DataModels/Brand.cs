namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Brand")]
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
