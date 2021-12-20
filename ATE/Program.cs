using Microsoft.Extensions.DependencyInjection;

namespace ATE
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            var services = startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetService<AppHost>()?.Run();
            }
        }
    }
}
