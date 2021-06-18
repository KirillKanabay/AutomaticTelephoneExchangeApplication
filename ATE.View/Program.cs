using ATE.Configurations.IoC;
using ATE.Views;
using Autofac;

namespace ATE
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ConfigureContainer>();
            
            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                container.Resolve<MainMenuView>().Show();
            }
        }
    }
}
