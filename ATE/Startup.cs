using ATE.Entities.Company.Creators;
using Microsoft.Extensions.DependencyInjection;

namespace ATE
{
    internal class Startup
    {
        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<AppHost>();
            services.AddScoped<AbstractCompanyCreator, MtsCompanyCreator>();

            return services;
        }
    }
}
