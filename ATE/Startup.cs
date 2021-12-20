using Microsoft.Extensions.DependencyInjection;

namespace ATE
{
    internal class Startup
    {
        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<AppHost>();

            return services;
        }
    }
}
