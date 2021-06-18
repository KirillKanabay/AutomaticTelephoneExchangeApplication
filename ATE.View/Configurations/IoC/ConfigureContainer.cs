using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Infrastructure.Data;
using ATE.Views;
using ATE.Views.Company;
using Autofac;

namespace ATE.Configurations.IoC
{
    public class ConfigureContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AteContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            LoadViews(builder);
            LoadHelpers(builder);
        }

        private void LoadViews(ContainerBuilder builder)
        {
            builder.RegisterType<ViewContainer>().AsSelf().SingleInstance();

            builder.RegisterType<MainMenuView>().AsSelf().InstancePerLifetimeScope();

            #region Company
            builder.RegisterType<CompanyMenuView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddCompanyView>().AsSelf().InstancePerLifetimeScope();
            #endregion

        }

        private void LoadHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<KeyEvent>().AsSelf().InstancePerDependency();
        }
    }
}
