using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Infrastructure.Data;
using ATE.Views;
using ATE.Views.Companies;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace ATE.Configurations.IoC
{
    public class ConfigureContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var options = new DbContextOptionsBuilder()
                    .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=ate_db;").Options;
                return new AteContext(options);
            }).AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));

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
            builder.RegisterType<ListCompanyView>().AsSelf().InstancePerLifetimeScope();
            
            #endregion

        }

        private void LoadHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<KeyEvent>().AsSelf().InstancePerDependency();
        }
    }
}
