using Inventory.Core.Factories;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Inventory.Core
{
    public static class CoreServiceRegistration
    {
        public static IHostBuilder AddCoreServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((builderContext, services) =>
            {
                services.AddStores()
                    .AddViewModels()
                    .AddServices();
            });

            return hostBuilder;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IModalNavigationService, ModalNavigationService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IInvoiceService, InvoiceService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            return services;
        }

        private static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddTransient<NavigationBarViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MessageViewModel>();
            services.AddTransient<CustomerViewModel>();
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<InvoiceViewModel>();
            services.AddTransient<InvoiceItemViewModel>();
            services.AddTransient<InvoicesListViewModel>();
            services.AddTransient<ListPageViewModel>();
            services.AddTransient<PriceViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<ProductsListViewModel>();
            services.AddTransient<RoleViewModel>();

            services.AddSingleton<Func<NavigationBarViewModel>>(serviceProvider => serviceProvider.GetRequiredService<NavigationBarViewModel>);
            services.AddSingleton<Func<LoginViewModel>>(serviceProvider => serviceProvider.GetRequiredService<LoginViewModel>);
            services.AddSingleton<Func<MessageViewModel>>(serviceProvider => serviceProvider.GetRequiredService<MessageViewModel>);
            services.AddSingleton<Func<CustomerViewModel>>(serviceProvider => serviceProvider.GetRequiredService<CustomerViewModel>);
            services.AddSingleton<Func<EmployeeViewModel>>(serviceProvider => serviceProvider.GetRequiredService<EmployeeViewModel>);
            services.AddSingleton<Func<HomeViewModel>>(serviceProvider => serviceProvider.GetRequiredService<HomeViewModel>);
            services.AddSingleton<Func<InvoiceViewModel>>(serviceProvider => serviceProvider.GetRequiredService<InvoiceViewModel>);
            services.AddSingleton<Func<InvoiceItemViewModel>>(serviceProvider => serviceProvider.GetRequiredService<InvoiceItemViewModel>);
            services.AddSingleton<Func<InvoicesListViewModel>>(serviceProvider => serviceProvider.GetRequiredService<InvoicesListViewModel>);
            services.AddSingleton<Func<ListPageViewModel>>(serviceProvider => serviceProvider.GetRequiredService<ListPageViewModel>);
            services.AddSingleton<Func<PriceViewModel>>(serviceProvider => serviceProvider.GetRequiredService<PriceViewModel>);
            services.AddSingleton<Func<ProductViewModel>>(serviceProvider => serviceProvider.GetRequiredService<ProductViewModel>);
            services.AddSingleton<Func<ProductsListViewModel>>(serviceProvider => serviceProvider.GetRequiredService<ProductsListViewModel>);
            services.AddSingleton<Func<RoleViewModel>>(serviceProvider => serviceProvider.GetRequiredService<RoleViewModel>);

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            return services;
        }

        private static IServiceCollection AddStores(this IServiceCollection services)
        {
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();
            services.AddSingleton<AccountStore>();
            services.AddTransient<ProductStore>();
            services.AddTransient<InvoiceStore>();
            services.AddTransient<InvoiceItemStore>();
            services.AddTransient<PriceStore>();

            services.AddSingleton<Func<ProductStore>>(serviceProvider => serviceProvider.GetRequiredService<ProductStore>);
            services.AddSingleton<Func<InvoiceStore>>(serviceProvider => serviceProvider.GetRequiredService<InvoiceStore>);
            services.AddSingleton<Func<InvoiceItemStore>>(serviceProvider => serviceProvider.GetRequiredService<InvoiceItemStore>);
            services.AddSingleton<Func<PriceStore>>(serviceProvider => serviceProvider.GetRequiredService<PriceStore>);

            return services;
        }
    }
}
