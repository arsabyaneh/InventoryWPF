using Inventory.Core.Exceptions;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class InvoiceViewModel : BaseViewModel
    {
        private readonly IModalNavigationService _ModalNavigationService;
        private readonly IInvoiceService _InvoiceService;
        private readonly IProductService _ProductService;
        private readonly ICustomerService _CustomerService;
        private readonly AccountStore _AccountStore;
        private readonly InvoiceItemStore _InvoiceItemStore;

        private InvoiceStore? _InvoiceStore;

        private string _Code;
        private DateTime? _InvoiceDate;
        private string _Discount;
        private string _ProductCode;
        private string _Quantity;
        private string _EmployeeName;
        private decimal? _TotalPrice = 0;
        private Employee? _Employee;
        private ObservableCollection<InvoiceItemViewModel> _InvoiceItems = new ObservableCollection<InvoiceItemViewModel>();
        private Invoice? _Invoice;

        public InvoiceViewModel(IModalNavigationService modalNavigationService, IInvoiceService invoiceService, IProductService productService, ICustomerService customerService, 
            AccountStore accountStore, InvoiceItemStore invoiceItemStore)
        {
            _ModalNavigationService = modalNavigationService;
            _InvoiceService = invoiceService;
            _ProductService = productService;
            _CustomerService = customerService;
            _AccountStore = accountStore;
            _InvoiceItemStore = invoiceItemStore;

            _InvoiceItemStore.InvoiceItemDeleted += InvoiceStore_InvoiceItemDeleted;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            AddProductCommand = new RelayCommand(AddProduct);
            ViewCommand = new RelayAsyncCommand(View, ex => throw ex);
            DeleteCommand = new RelayAsyncCommand(Delete, ex => throw ex);
        }

        public override Task<BaseViewModel> Initialise(IStore store, EntityState entityState, object? entity = null)
        {
            _InvoiceStore = store is InvoiceStore invoiceStore ? invoiceStore : null;
            EntityState = entityState;
            Invoice = entity is Invoice invoice ? invoice : null;

            return base.Initialise(store, entityState, entity);
        }

        public string? Code { get => _Code; set => SetProperty(ref _Code, value); }
        public DateTime? InvoiceDate { get => _InvoiceDate; set => SetProperty(ref _InvoiceDate, value); }
        public string? Discount { get => _Discount; set => SetProperty(ref _Discount, value); }
        public string ProductCode { get => _ProductCode; set => SetProperty(ref _ProductCode, value); }
        public string Quantity { get => _Quantity; set => SetProperty(ref _Quantity, value); }
        public string EmployeeName { get => _EmployeeName; set => SetProperty(ref _EmployeeName, value); }
        public decimal? TotalPrice { get => _TotalPrice; set => SetProperty(ref _TotalPrice, value); }
        public ObservableCollection<InvoiceItemViewModel> InvoiceItemViewModels { get => _InvoiceItems; set => SetProperty(ref _InvoiceItems, value); }

        private ObservableCollection<InvoiceItemViewModel> RemovedInvoiceItems { get; set; } = new ObservableCollection<InvoiceItemViewModel>();
        private Employee? Employee
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
        public ICommand ViewCommand { get; }
        public ICommand DeleteCommand { get; }

        public Invoice? Invoice
        {
            get
            {
                _Invoice = new Invoice
                {
                    Id = _Invoice != null ? _Invoice.Id : 0,
                    Code = Code ?? string.Empty,
                    InvoiceDate = InvoiceDate != null ? InvoiceDate.Value : DateTime.Now,
                    Discount = decimal.Parse(Discount ?? string.Empty),
                    EmployeeId = Employee != null ? Employee.Id : 0,
                    CustomerId = _CustomerService.CashDeskCustomer != null ? _CustomerService.CashDeskCustomer.Id : 0,
                    InvoiceItems = GetInvoiceItems(InvoiceItemViewModels)
                };

                return _Invoice;
            }

            set
            {
                _Invoice = value;

                Code = _Invoice?.Code;
                InvoiceDate = _Invoice != null ? _Invoice.InvoiceDate : DateTime.Now;
                Discount = _Invoice?.Discount.ToString();
                Employee = _Invoice != null ? _Invoice?.Employee : _AccountStore.CurrentAccount.Employee;
                InvoiceItemViewModels = SetInvoiceItemViewModels(_Invoice?.InvoiceItems);
            }
        }

        private void Ok()
        {
            _InvoiceService?.Save(Invoice);
            _InvoiceStore?.UpdateInvoice(this);
            _ModalNavigationService?.Close();
        }

        private void Cancel()
        {
            _InvoiceStore?.UpdateInvoice(this);
            _ModalNavigationService.Close();
        }

        private void AddProduct()
        {
            Product product = _ProductService.LoadProduct(ProductCode);
            if (product != null)
            {
                decimal price = int.Parse(Quantity) * product.Prices.OrderByDescending(x => x.PriceDate).First().Sell;
                InvoiceItemViewModels.Add(new InvoiceItemViewModel(_InvoiceItemStore)
                {
                    Product = product,
                    Quantity = Quantity,
                    Price = price.ToString()
                });
                TotalPrice = TotalPrice + price;
            }
        }

        private void InvoiceStore_InvoiceItemDeleted(InvoiceItemViewModel invoiceItemViewModel)
        {
            InvoiceItemViewModels.Remove(invoiceItemViewModel);
            TotalPrice -= decimal.Parse(invoiceItemViewModel.Price);

            if (invoiceItemViewModel.Id != 0)
                RemovedInvoiceItems.Add(invoiceItemViewModel);
        }

        private ObservableCollection<InvoiceItemViewModel> SetInvoiceItemViewModels(IEnumerable<InvoiceItem>? invoiceItems)
        {
            ObservableCollection<InvoiceItemViewModel> invoiceItemViewModels = new ObservableCollection<InvoiceItemViewModel>();
            
            if (invoiceItems != null)
            {
                TotalPrice = 0;
                foreach (var item in invoiceItems)
                {
                    decimal price = item.Quantity * _ProductService.LoadProductSellPrice(item.Product.Code);

                    invoiceItemViewModels.Add(new InvoiceItemViewModel(_InvoiceItemStore)
                    {
                        Id = item.Id,
                        Product = item.Product,
                        Quantity = item.Quantity.ToString(),
                        Price = price.ToString()
                    });
                    TotalPrice += price;
                }
            }

            return invoiceItemViewModels;
        }

        private ICollection<InvoiceItem> GetInvoiceItems(IEnumerable<InvoiceItemViewModel> invoiceItemViewModels)
        {
            List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

            if (RemovedInvoiceItems != null)
            {
                foreach (var item in RemovedInvoiceItems)
                {
                    invoiceItems.Add(new InvoiceItem
                    {
                        Id = item.Id,
                        EntityState = Microsoft.EntityFrameworkCore.EntityState.Deleted
                    });
                }
            }

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

        private async Task View()
        {
            if (_Invoice != null)
            {
                InvoiceItemViewModels = SetInvoiceItemViewModels(await _InvoiceService.LoadInvoiceItems(_Invoice.Id));

                _ModalNavigationService.Navigate(() => this);
            }
        }

        private async Task Delete()
        {
            if (_Invoice != null)
            {
                try
                {
                    await _InvoiceService.Delete(_Invoice.Id);
                    _InvoiceStore?.DeleteInvoice(this);
                }
                catch (DatabaseException ex)
                {
                    string message = ex.Message;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
        }
    }
}
