namespace Inventory.EntityFramework.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Price")]
    public partial class Price :  BaseDataModel
    {
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime PriceDate { get; set; }

        public decimal Buy { get; set; }

        public decimal Sell { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
