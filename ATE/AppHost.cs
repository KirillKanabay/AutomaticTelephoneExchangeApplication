using ATE.Entities.ATE;
using ATE.Entities.Company;
using ATE.Entities.Company.Creators;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Port;
using ATE.Entities.Terminal;
using ATE.Entities.Users;

namespace ATE
{
    internal class AppHost
    {
        private readonly AbstractCompanyCreator _companyCreator;

        public AppHost(AbstractCompanyCreator companyCreator)
        {
            _companyCreator = companyCreator;
        }

        public void Run()
        {
            var terminal1 = new Terminal();
            var terminal2 = new Terminal();

            terminal1.Number = "1";
            terminal2.Number = "2";

            var terminalView1 = new TerminalView(terminal1);
            var terminalView2 = new TerminalView(terminal2);

            IPortController portController = new PortController(16);
            Station station = new Station(portController);

            terminal1.ConnectToStation(station);
            terminal2.ConnectToStation(station);

            terminal1.Call("2");

            if (terminal2.CurrentCall != null)
            {
                terminal2.EndCall();
            }

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


            BaseCompany company = _companyCreator.Create();

            Client client1 = company.RegisterClient(user1, new EasySayTariff());
            Client client2 = company.RegisterClient(user2, new EasySayTariff());


            //
            // Subscriber subscriber1 = company.Subscribe(new SubscriberFactory(user1));
            // Subscriber subscriber2 = company.Subscribe(new SubscriberFactory(user2));
            //
            // TerminalView terminal1View = new TerminalView(subscriber1.Terminal);
            // TerminalView terminal2View = new TerminalView(subscriber2.Terminal);
            //
            // var ate = company.AteCollection.FirstOrDefault();
            //
            // subscriber1.BillingAccount.Deposit(5);
            // subscriber2.BillingAccount.Deposit(5);
            //
            // subscriber1.Terminal.ConnectTo(ate);
            // subscriber2.Terminal.ConnectTo(ate);
            //
            // subscriber1.Terminal.CallTo(subscriber2.Terminal.Number);
            // if (subscriber1.Terminal.CurrentCall != null)
            // {
            //     Thread.Sleep(5000);
            //     subscriber1.Terminal.EndCall();
            // }
            //
            // subscriber2.Terminal.CallTo(subscriber1.Terminal.Number);
            // if (subscriber1.Terminal.CurrentCall != null)
            // {
            //     Thread.Sleep(3000);
            //     subscriber2.Terminal.EndCall();
            // }
            //
            // var presenter1 = new CallPresenter(new CallReporter(company.BillingSystem, subscriber1.BillingAccount));
            // var presenter2 = new CallPresenter(new CallReporter(company.BillingSystem, subscriber2.BillingAccount));
            //
            // presenter1.Present();
            // Console.WriteLine(new string('=', 80));
            // presenter2.Present();
        }
    }
}
