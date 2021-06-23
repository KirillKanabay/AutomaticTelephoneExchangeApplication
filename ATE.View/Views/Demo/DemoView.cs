using ATE.Core.Entities;
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

            Contract client1Contract = new Contract(numGenerator.Generate(company), client1.Id, tariff.Id, company.Id); //todo: передавать ссылки
            Contract client2Contract = new Contract(numGenerator.Generate(company), client2.Id, tariff.Id, company.Id);
            
            ITerminal terminal1 = new Phone(client1Contract);
            ITerminal terminal2 = new Phone(client2Contract);
            
            TerminalView terminal1View = new TerminalView(terminal1);
            
            AutomaticTelephoneExchange ate = new AutomaticTelephoneExchange(company, 256); //todo: проверка количества портов
            
            terminal1.ConnectToPort(ate);
            terminal2.ConnectToPort(ate);
            
            terminal2.CallTo(terminal1.Number);
        }
        
        
    }
}