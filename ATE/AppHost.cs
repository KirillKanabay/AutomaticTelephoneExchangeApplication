using System;
using ATE.Abstractions.Factories;
using ATE.Entities.Calls;
using ATE.Entities.Company;
using ATE.Entities.Terminal;
using ATE.Entities.Users;
using ATE.Enums;

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
            User user1 = new User()
            {
                FirstName = "Kirill", 
                LastName = "Kanabay"
            };
            User user2 = new User()
            {
                FirstName = "Ivan",
                LastName = "Ivanov"
            };

            BaseCompany company = _companyFactory.CreateCompany();

            Client client1 = company.RegisterClient(user1);
            Client client2 = company.RegisterClient(user2);

            var terminal1 = client1.Terminal;
            var terminal2 = client2.Terminal;
            
            var terminalView1 = new TerminalView(terminal1);
            var terminalView2 = new TerminalView(terminal2);

            var station = _stationFactory.CreateStation();

            company.AddStation(station);

            company.BillingSystem.Deposit(client1, 5.0m);
            company.BillingSystem.Deposit(client2, 5.0m);

            terminal1.ConnectToStation(station);
            terminal2.ConnectToStation(station);

            terminal1.Call(client2.PhoneNumber);

            if (terminal2.CurrentCall != null)
            {
                terminal2.EndCall();
            }

            terminal2.Call(client1.PhoneNumber);
            
            if (terminal2.CurrentCall != null)
            {
                terminal1.EndCall();
            }

            
            _callPresenter.Present(company.BillingSystem, client1);
            Console.WriteLine(new string('=', 80));
            _callPresenter.Present(company.BillingSystem, client2, CallSortType.Price);
        }
    }
}
