namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvoiceItem")]
    public partial class InvoiceItem : BaseDataModel
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public long InvoiceId { get; set; }

        public long ProductId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }
    }
}
