using Inventory.Core.Services;
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

        public HomeViewModel(INavigationService navigationService, INavigationService modalNavigationService, IAuthenticationService authenticationService, IProductService productService, IEmployeeService employeeService, ICustomerService customerService)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _ProductService = productService;
            _EmployeeService = employeeService;
            _CustomerService = customerService;
            _AuthenticationService = authenticationService;

            ViewModelType = ViewModelType.Home;

            AddProductCommand = new RelayCommand(AddProduct);
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            AddRoleCommand = new RelayCommand(AddRole);
            AddCustomerCommand = new RelayCommand(AddCustomer);
        }

        public ICommand AddProductCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand AddRoleCommand { get; }
        public ICommand AddCustomerCommand { get; }

        private void AddProduct()
        {
            _ModalNavigationService.Navigate(() => new ProductViewModel(_ModalNavigationService, _ProductService)
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
    }
}
