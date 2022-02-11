namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Invoice")]
    public partial class Invoice : BaseDataModel
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal Discount { get; set; }

        public long CustomerId { get; set; }

        public long EmployeeId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
