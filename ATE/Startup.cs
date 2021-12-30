using ATE.Abstractions.Factories;
using ATE.Entities.Calls;
using ATE.Factories;
using ATE.Generators;
using ATE.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ATE
{
    internal class Startup
    {
        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<AppHost>();
            services.AddTransient<AbstractCompanyFactory, DefaultCompanyFactory>();
            services.AddTransient<IPhoneNumberGenerator, PhoneNumberGenerator>();
            services.AddTransient<AbstractTerminalFactory, TerminalFactory>();
            services.AddTransient<AbstractContractFactory, DefaultContractFactory>();
            services.AddTransient<AbstractClientFactory, ClientFactory>();
            services.AddTransient<AbstractStationFactory, StationFactory>();
            services.AddTransient<AbstractCallInformationComparerFactory, CallInformationComparerFactory>();
            services.AddTransient<ICallPresenter, CallPresenter>();

            return services;
        }
    }
}
