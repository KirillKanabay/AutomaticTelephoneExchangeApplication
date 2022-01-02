using System;
using System.Linq;
using System.Threading;
using ATE.Abstractions.Domain.Billings;
using ATE.Abstractions.Domain.Calls;
using ATE.Abstractions.Domain.Station;
using ATE.Abstractions.Factories;
using ATE.Constants;
using ATE.Domain.Company;
using ATE.Domain.Terminal;
using ATE.Domain.Users;
using ATE.Enums;
using ATE.Views;

namespace ATE
{
    internal class AppHost
    {
        private readonly AbstractCompanyFactory _companyFactory;
        private readonly AbstractStationFactory _stationFactory;
        private readonly ICallPresenter _callPresenter;
        
        public AppHost(AbstractCompanyFactory companyFactory, 
            AbstractStationFactory stationFactory,
            ICallPresenter callPresenter)
        {
            _companyFactory = companyFactory;
            _stationFactory = stationFactory;
            _callPresenter = callPresenter;
        }

        public void Run()
        {
            var user1 = new User
            {
                FirstName = "Kirill", 
                LastName = "Kanabay",
            };
            var user2 = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
            };
            var user3 = new User
            {
                FirstName = "Peter",
                LastName = "Parker",
            };

            var company = _companyFactory.CreateCompany();

            var client1 = company.RegisterClient(user1);
            var client2 = company.RegisterClient(user2);
            var client3 = company.RegisterClient(user3);

            company.AddStation(_stationFactory.CreateStation());

            company.BillingSystem.Deposit(client1, 0.5m);
            company.BillingSystem.Deposit(client2, 5.0m);
            
            var terminalView1 = new TerminalView(client1.Terminal);
            var terminalView2 = new TerminalView(client2.Terminal);

            var station = company.Stations.FirstOrDefault();

            Call(client1, client2, station);
            Call(client2, client1, station);
            Call(client1, client3, station);
            Call(client1,client3, station);
            Call(client1, client1, station);

            PrintClientCalls(client1, company.BillingSystem);
            PrintClientCalls(client2, company.BillingSystem);
            PrintClientCalls(client3, company.BillingSystem);
        }

        private void Call(Client source, Client target, BaseStation station)
        {
            Console.WriteLine($"==== Call from {source.User} to {target.User} ====\n");
            source.Terminal.ConnectToStation(station);
            target.Terminal.ConnectToStation(station);

            source.Terminal.Call(target.PhoneNumber);

            if (source.Terminal.CurrentCall != null)
            {
                Thread.Sleep(DataConstants.DefaultCallDuration);
                source.Terminal.EndCall();
            }

            Console.WriteLine();

            source.Terminal.Disconnect();
            target.Terminal.Disconnect();
        }

        private void PrintClientCalls(Client client, BaseBillingSystem billingSystem)
        {
            Console.WriteLine($"{client.User} calls");
            _callPresenter.Present(billingSystem, client);
            Console.WriteLine();
        }
    }
}
