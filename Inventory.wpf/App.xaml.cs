using Inventory.Core;
using Inventory.Core.Services;
using Inventory.Core.ViewModels;
using Inventory.EntityFramework;
using Inventory.wpf.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Inventory.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddInfrastructureServices()
                .AddCoreServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var navigationService = _host.Services.GetRequiredService<INavigationService>();
            var loginViewModel    = _host.Services.GetRequiredService<LoginViewModel>();
            var mainViewModel     = _host.Services.GetRequiredService<MainViewModel>();

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
