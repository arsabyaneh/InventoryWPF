using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;
        private readonly IAuthenticationService _AuthenticationService;
        private readonly IProductService _ProductService;
        private readonly IEmployeeService _EmployeeService;
        private readonly ICustomerService _CustomerService;
        private readonly IInvoiceService _InvoiceService;
        private readonly AccountStore _AccountStore;
        private readonly Func<ProductStore> _CreateProductStore;

        public HomeViewModel(INavigationService navigationService, INavigationService modalNavigationService, IAuthenticationService authenticationService, IProductService productService, 
            IEmployeeService employeeService, ICustomerService customerService, IInvoiceService invoiceService, 
            AccountStore accountStore, Func<ProductStore> createProductStore)
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

            ViewModelType = ViewModelType.Home;

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
            _ModalNavigationService.Navigate(() => new ProductViewModel(_ModalNavigationService, _ProductService, _CreateProductStore(), null)
            {
                ControlWidth = 700
            });
        }

        private void AddEmployee()
        {
            _ModalNavigationService.Navigate(() => new EmployeeViewModel(_ModalNavigationService, _AuthenticationService, _EmployeeService) 
            {
                ControlWidth = 500
            });
        }

        private void AddRole()
        {
            _ModalNavigationService.Navigate(() => new RoleViewModel(_ModalNavigationService, _EmployeeService)
            {
                ControlWidth = 500
            });
        }

        private void AddCustomer()
        {
            _ModalNavigationService.Navigate(() => new CustomerViewModel(_ModalNavigationService, _CustomerService)
            {
                ControlWidth = 500
            });
        }

        private void AddInvoice()
        {
            _ModalNavigationService.Navigate(() => new InvoiceViewModel(_ModalNavigationService, _InvoiceService, _ProductService, _CustomerService, _AccountStore, null)
            {
                ControlWidth = 700
            });
        }

        private void ViewProducts()
        {
            _NavigationService.Navigate(() => new ProductsListViewModel(_NavigationService, _ModalNavigationService, _ProductService, _CreateProductStore));
        }

        private void ViewInvoices()
        {
            _NavigationService.Navigate(() => new InvoicesListViewModel(_NavigationService, _ModalNavigationService, _InvoiceService, _ProductService, _CustomerService, _AccountStore));
        }
    }
}
