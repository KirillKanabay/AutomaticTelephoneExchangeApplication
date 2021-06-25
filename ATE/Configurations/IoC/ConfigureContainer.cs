using ATE.Core.Generators;
using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Infrastructure.Data;
using ATE.Views;
using ATE.Views.Clients;
using ATE.Views.Companies;
using ATE.Views.Contracts;
using ATE.Views.Demo;
using ATE.Views.Tariffs;
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

            builder.RegisterType<DemoView>().AsSelf().InstancePerLifetimeScope();
           
            #region Company

            builder.RegisterType<CompanyMenuView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddCompanyView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ListCompanyView>().AsSelf().InstancePerLifetimeScope();
            
            #endregion

            #region Client

            builder.RegisterType<ListClientView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ClientMenuView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddClientView>().AsSelf().InstancePerLifetimeScope();

            #endregion

            #region Contract

            builder.RegisterType<ContractMenuView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddContractView>().AsSelf().InstancePerLifetimeScope();
            #endregion
            
            #region Tariff

            builder.RegisterType<TariffMenuView>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddTariffView>().AsSelf().InstancePerLifetimeScope();

            #endregion
        }

        private void LoadHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<PhoneNumberGenerator>().As<IPhoneNumberGenerator>().SingleInstance();
            builder.RegisterType<KeyEvent>().AsSelf().InstancePerDependency();
        }
    }
}
