using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class InvoiceViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly IProductService _ProductService;

        private string _Code;
        private DateTime _InvoiceDate;
        private string _Discount;
        private string _ProductCode;
        private string _Quantity;
        private ObservableCollection<InvoiceItemViewModel> _InvoiceItems = new ObservableCollection<InvoiceItemViewModel>();
        private Invoice _Invoice;

        public InvoiceViewModel(INavigationService navigationService, IProductService productService)
        {
            _NavigationService = navigationService;
            _ProductService = productService;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            AddProductCommand = new RelayCommand(AddProduct);
        }

        public string Code { get => _Code; set => SetProperty(ref _Code, value); }
        public DateTime InvoiceDate { get => _InvoiceDate; set => SetProperty(ref _InvoiceDate, value); }
        public string Discount { get => _Discount; set => SetProperty(ref _Discount, value); }
        public string ProductCode { get => _ProductCode; set => SetProperty(ref _ProductCode, value); }
        public string Quantity { get => _Quantity; set => SetProperty(ref _Quantity, value); }
        public ObservableCollection<InvoiceItemViewModel> InvoiceItems { get => _InvoiceItems; set => SetProperty(ref _InvoiceItems, value); }

        private ObservableCollection<InvoiceItemViewModel> RemovedInvoiceItems { get; set; }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddProductCommand { get; }

        public Invoice Invoice
        {
            get
            {
                _Invoice = new Invoice
                {
                    Id = _Invoice != null ? _Invoice.Id : 0,
                    Code = Code,
                    InvoiceDate = InvoiceDate,
                    Discount = decimal.Parse(Discount),
                };

                return _Invoice;
            }

            set
            {
                _Invoice = value;
            }
        }

        private void Ok()
        {   
            _NavigationService.Close();
        }

        private void Cancel()
        {
            _NavigationService.Close();
        }

        private void AddProduct()
        {
            Product product = _ProductService.LoadProduct(ProductCode);
            if (product != null)
            {
                decimal price = int.Parse(Quantity) * product.Prices.OrderByDescending(x => x.PriceDate).First().Sell;
                InvoiceItems.Add(new InvoiceItemViewModel
                {
                    ProductTitle = product.Title,
                    Quantity = Quantity,
                    Price = price.ToString()
                });
            }
        }
    }
}
