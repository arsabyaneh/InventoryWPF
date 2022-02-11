using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
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
            NavigationService navigationService = new NavigationService(navigationStore, navService => new NavigationBarViewModel(navService));
            ModalNavigationService modalNavigationService = new ModalNavigationService(modalNavigationStore);
            ProductService productService = new ProductService();
            EmployeeService employeeService = new EmployeeService();
            MainViewModel mainViewModel = new MainViewModel(navigationStore, modalNavigationStore);


            HomeViewModel homeViewModel = new HomeViewModel(navigationService, modalNavigationService, productService, employeeService);
            navigationService.Navigate(() => homeViewModel);

            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();


            base.OnStartup(e);
        }
    }
}
