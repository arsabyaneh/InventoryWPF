using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Inventory.wpf.Extensions
{
    public static class AddConfigurationHostBuilder
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("appsettings.json");
            });

            return host;
        }
    }
}
