using Inventory.Core.Factories;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels.Base;
using System;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly IModalNavigationService _ModalNavigationService;
        private readonly IAuthenticationService _AuthenticationService;
        private readonly IProductService _ProductService;
        private readonly IEmployeeService _EmployeeService;
        private readonly ICustomerService _CustomerService;
        private readonly IInvoiceService _InvoiceService;
        private readonly AccountStore _AccountStore;
        private readonly Func<ProductStore> _CreateProductStore;
        private readonly Func<InvoiceStore> _CreateInvoiceStore;
        private readonly Func<InvoiceItemStore> _CreateInvoiceItemStore;
        private readonly Func<PriceStore> _CreatePriceStore;
        private readonly IViewModelFactory _ViewModelFactory;

        public HomeViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IAuthenticationService authenticationService, 
            IProductService productService, IEmployeeService employeeService, ICustomerService customerService, IInvoiceService invoiceService, 
            AccountStore accountStore, Func<ProductStore> createProductStore, Func<InvoiceStore> createInvoiceStore, 
            Func<InvoiceItemStore> createInvoiceItemStore, Func<PriceStore> createPriceStore,
            IViewModelFactory viewModelFactory)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _AuthenticationService = authenticationService;
            _ProductService = productService;
            _EmployeeService = employeeService;
            _CustomerService = customerService;
            _InvoiceService = invoiceService;
            _AccountStore = accountStore;
            _CreateProductStore = createProductStore;
            _CreateInvoiceStore = createInvoiceStore;
            _CreateInvoiceItemStore = createInvoiceItemStore;
            _CreatePriceStore = createPriceStore;
            _ViewModelFactory = viewModelFactory;

            ViewModelType = ViewType.Home;

            AddProductCommand = new RelayCommand(AddProduct);
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            AddRoleCommand = new RelayCommand(AddRole);
            AddCustomerCommand = new RelayCommand(AddCustomer);
            AddInvoiceCommand = new RelayCommand(AddInvoice);
            ViewProductsCommand = new RelayCommand(ViewProducts);
            ViewInvoicesCommand = new RelayCommand(ViewInvoices);
        }

        public ICommand AddProductCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand AddRoleCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand AddInvoiceCommand { get; }
        public ICommand ViewProductsCommand { get; }
        public ICommand ViewInvoicesCommand { get; }

        private void AddProduct()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.Product) is ProductViewModel productViewModel)
            {
                productViewModel.Initialise(_CreateProductStore(), Microsoft.EntityFrameworkCore.EntityState.Added, null);
                productViewModel.ControlWidth = 700;
                _ModalNavigationService.Navigate(() => productViewModel);
            }
        }

        private void AddEmployee()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.Employee) is EmployeeViewModel employeeViewModel)
            {
                employeeViewModel.ControlWidth = 500;
                _ModalNavigationService.Navigate(() => employeeViewModel);
            }
        }

        private void AddRole()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.Role) is RoleViewModel roleViewModel)
            {
                roleViewModel.ControlWidth = 500;
                _ModalNavigationService.Navigate(() => roleViewModel);
            }
        }

        private void AddCustomer()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.Customer) is CustomerViewModel customerViewModel)
            {
                customerViewModel.ControlWidth = 500;
                _ModalNavigationService.Navigate(() => customerViewModel);
            }
        }

        private void AddInvoice()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.Invoice) is InvoiceViewModel invoiceViewModel)
            {
                invoiceViewModel.Initialise(_CreateInvoiceStore(), Microsoft.EntityFrameworkCore.EntityState.Added, null);
                invoiceViewModel.ControlWidth = 700;
                _ModalNavigationService.Navigate(() => invoiceViewModel);
            }
        }

        private void ViewProducts()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.ProductsList) is ProductsListViewModel productsListViewModel)
            {
                productsListViewModel.ControlWidth = 500;
                _NavigationService.Navigate(() => productsListViewModel);
            }
        }

        private void ViewInvoices()
        {
            if (_ViewModelFactory.CreateViewModel(ViewType.InvoicesList) is InvoicesListViewModel invoicesListViewModel)
            {
                invoicesListViewModel.ControlWidth = 500;
                _NavigationService.Navigate(() => invoicesListViewModel);
            }
        }
    }
}
