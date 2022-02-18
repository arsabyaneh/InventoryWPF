using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class InvoiceItemViewModel : BaseViewModel
    {
        private string _ProductTitle;
        private string _Quantity;
        private string _Price;
        private Product _Product;

        public long Id { get; set; }
        public string ProductTitle { get => _ProductTitle; set => SetProperty(ref _ProductTitle, value); }
        public string Quantity { get => _Quantity; set => SetProperty(ref _Quantity, value); }
        public string Price { get => _Price; set => SetProperty(ref _Price, value); }

        public Product Product
        {
            get => _Product;
            set
            {
                _Product = value;
                ProductTitle = Product?.Title ?? string.Empty;
            }
        }
    }
}
