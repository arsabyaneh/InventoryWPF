using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Inventory.EntityFramework
{
    public static class InfrastructureServiceRegistration
    {
        public static IHostBuilder AddInfrastructureServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((builderContext, services) =>
            {
                services.AddDbContext<InventoryDbContext>(options =>
                {
                    options.UseSqlServer(builderContext.Configuration.GetConnectionString("InventoryConnectionString") ?? throw new ArgumentNullException("InventoryConnectionString"));
                }, contextLifetime: ServiceLifetime.Transient);
                
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<Func<IUnitOfWork>>(serviceProvider => serviceProvider.GetRequiredService<IUnitOfWork>);
            });

            return hostBuilder;
        }
    }
}
