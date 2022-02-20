using Inventory.Core.Services;
using Inventory.Core.Stores;
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
        private readonly IInvoiceService _InvoiceService;
        private readonly IProductService _ProductService;
        private readonly ICustomerService _CustomerService;
        private readonly AccountStore _AccountStore;

        private string _Code;
        private DateTime? _InvoiceDate;
        private string _Discount;
        private string _ProductCode;
        private string _Quantity;
        private string _EmployeeName;
        private decimal? _TotalPrice = 0;
        private Employee _Employee;
        private ObservableCollection<InvoiceItemViewModel> _InvoiceItems = new ObservableCollection<InvoiceItemViewModel>();
        private Invoice _Invoice;

        public InvoiceViewModel(INavigationService navigationService, IInvoiceService invoiceService, IProductService productService, ICustomerService customerService, 
            AccountStore accountStore, Invoice invoice)
        {
            _NavigationService = navigationService;
            _InvoiceService = invoiceService;
            _ProductService = productService;
            _CustomerService = customerService;
            _AccountStore = accountStore;

            Invoice = invoice;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            AddProductCommand = new RelayCommand(AddProduct);
        }

        public string Code { get => _Code; set => SetProperty(ref _Code, value); }
        public DateTime? InvoiceDate { get => _InvoiceDate; set => SetProperty(ref _InvoiceDate, value); }
        public string Discount { get => _Discount; set => SetProperty(ref _Discount, value); }
        public string ProductCode { get => _ProductCode; set => SetProperty(ref _ProductCode, value); }
        public string Quantity { get => _Quantity; set => SetProperty(ref _Quantity, value); }
        public string EmployeeName { get => _EmployeeName; set => SetProperty(ref _EmployeeName, value); }
        public decimal? TotalPrice { get => _TotalPrice; set=> SetProperty(ref _TotalPrice, value); }
        public ObservableCollection<InvoiceItemViewModel> InvoiceItemViewModels { get => _InvoiceItems; set => SetProperty(ref _InvoiceItems, value); }

        private ObservableCollection<InvoiceItemViewModel> RemovedInvoiceItems { get; set; }
        private Employee Employee
        {
            get => _Employee;
            set
            {
                _Employee = value;
                EmployeeName = _Employee != null ? $"{_Employee.FirstName} {_Employee.LastName}" : string.Empty;
            }
        }

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
                    InvoiceDate = InvoiceDate != null ? InvoiceDate.Value : DateTime.Now,
                    Discount = decimal.Parse(Discount),
                    TotalPrice = TotalPrice,
                    EmployeeId = Employee.Id,
                    CustomerId = _CustomerService.CashDeskCustomer.Id,
                    InvoiceItems = GetInvoiceItems(InvoiceItemViewModels)
                };

                return _Invoice;
            }

            set
            {
                _Invoice = value;

                Code = _Invoice?.Code;
                InvoiceDate = _Invoice?.InvoiceDate;
                Discount = _Invoice?.Discount.ToString();
                TotalPrice = _Invoice?.TotalPrice;
                Employee = _Invoice != null ? _Invoice?.Employee : _AccountStore.CurrentAccount.Employee;
                InvoiceItemViewModels = SetInvoiceItemViewModels(_Invoice?.InvoiceItems);
            }
        }

        private void Ok()
        {
            _InvoiceService.Save(Invoice);
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
                InvoiceItemViewModels.Add(new InvoiceItemViewModel
                {
                    Product = product,
                    Quantity = Quantity,
                    Price = price.ToString()
                });
                TotalPrice = TotalPrice + price;
            }
        }

        private ObservableCollection<InvoiceItemViewModel> SetInvoiceItemViewModels(IEnumerable<InvoiceItem> invoiceItems)
        {
            ObservableCollection<InvoiceItemViewModel> invoiceItemViewModels = new ObservableCollection<InvoiceItemViewModel>();
            
            if (invoiceItems != null)
            {
                foreach (var item in invoiceItems)
                {
                    invoiceItemViewModels.Add(new InvoiceItemViewModel
                    {
                        Id = item.Id,
                        Product = item.Product,
                        Quantity = item.Quantity.ToString(),
                        Price = _ProductService.LoadProductSellPrice(item.Product.Code).ToString()
                    });
                }
            }

            return invoiceItemViewModels;
        }

        private ICollection<InvoiceItem> GetInvoiceItems(IEnumerable<InvoiceItemViewModel> invoiceItemViewModels)
        {
            List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

            if (invoiceItemViewModels != null)
            {
                foreach (var item in invoiceItemViewModels)
                {
                    invoiceItems.Add(new InvoiceItem
                    {
                        Id = item.Id,
                        ProductId = item.Product.Id,
                        Quantity = int.Parse(item.Quantity),
                    });
                }
            }

            return invoiceItems;
        }
    }
}
