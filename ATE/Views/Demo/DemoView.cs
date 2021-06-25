using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;
using ATE.Generators;
using ATE.Views.Base;
using ATE.Views.Terminals;

namespace ATE.Views.Demo
{
    public class DemoView : BaseView
    {
        private readonly IRepository<Client> _clientRepo;
        private readonly IRepository<Contract> _contractRepo;

        public DemoView() : base("Демонстрация работы")
        {
            
        }
        
        public DemoView(IRepository<Client> clientRepo, IRepository<Contract> contractRepo) : base("Демонстрация работы")
        {
            _clientRepo = clientRepo;
            _contractRepo = contractRepo;
        }
        
        
        public override void Show()
        {
            Client client1 = new Client("Kirill", "Kanabay");
            Client client2 = new Client("Ivan", "Ivanov");

            Tariff tariff = new Tariff("Light", 0.05m);
            Company company = new Company("MTC", "375", "29");

            IPhoneNumberGenerator numGenerator = new PhoneNumberGenerator();

            Contract client1Contract = new Contract(numGenerator.Generate(company), tariff, client1, company);
            Contract client2Contract = new Contract(numGenerator.Generate(company), tariff, client2, company);
            
            BaseTerminal terminal1 = new Phone(client1Contract);
            BaseTerminal terminal2 = new Phone(client1Contract);
            BaseTerminal terminal3 = new Phone(client2Contract);
            
            TerminalView terminal1View = new TerminalView(terminal1);
            TerminalView terminal2View = new TerminalView(terminal2);
            
            AutomaticTelephoneExchange ate = new AutomaticTelephoneExchange(company, 256);
            
            terminal1.ConnectTo(ate);
            terminal2.ConnectTo(ate);
            
            terminal1.CallTo(terminal2.Number);
            // terminal2.CallTo(terminal1.Number);
            
        }
        
        
    }
}