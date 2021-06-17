using ATE.Helpers;
using ATE.Infrastructure.Data;
using ATE.Views;
using Autofac;

namespace ATE.Configurations.IoC
{
    public class ConfigureContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AteContext>().AsSelf().InstancePerLifetimeScope();

            LoadViews(builder);
            LoadHelpers(builder);
        }

        private void LoadViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainMenuView>().AsSelf().InstancePerLifetimeScope();
        }

        private void LoadHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<KeyEvent>().AsSelf().InstancePerDependency();
        }
    }
}
