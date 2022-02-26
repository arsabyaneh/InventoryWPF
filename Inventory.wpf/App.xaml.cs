using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            ModalNavigationStore modalNavigationStore = new ModalNavigationStore();
            AccountStore accountStore = new AccountStore();
            Func<ProductStore> createProductStore = () => new ProductStore();
            Func<InvoiceStore> createInvoiceStore = () => new InvoiceStore();
            NavigationService navigationService = new NavigationService(navigationStore, navService => new NavigationBarViewModel(navService, accountStore));
            ModalNavigationService modalNavigationService = new ModalNavigationService(modalNavigationStore);
            ProductService productService = new ProductService();
            EmployeeService employeeService = new EmployeeService();
            CustomerService customerService = new CustomerService();
            InvoiceService invoiceService = new InvoiceService();
            AuthenticationService authenticationService = new AuthenticationService(employeeService, new PasswordHasher());
            MainViewModel mainViewModel = new MainViewModel(navigationStore, modalNavigationStore);

            HomeViewModel homeViewModel = new HomeViewModel(navigationService, modalNavigationService, authenticationService, productService, employeeService, customerService, invoiceService, 
                accountStore, createProductStore, createInvoiceStore);
            LoginViewModel loginViewModel = new LoginViewModel(navigationService, authenticationService, homeViewModel, accountStore);
            navigationService.Navigate(() => loginViewModel);

            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();


            base.OnStartup(e);
        }
    }
}
