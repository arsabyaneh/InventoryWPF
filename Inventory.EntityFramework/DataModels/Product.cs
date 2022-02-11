namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product : BaseDataModel
    {
        public Product()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
            Prices = new HashSet<Price>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public long BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
    }
}
