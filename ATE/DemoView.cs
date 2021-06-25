using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Billing;
using ATE.Core.Factories;
using ATE.Core.Generators;
using ATE.Core.Interfaces;
using ATE.Views.Terminals;

namespace ATE
{
    public class DemoView
    {
        public void Show()
        {
            User user1 = new User("Kirill", "Kanabay");
            User user2 = new User("Ivan", "Ivanov");
            
            Company company = new Company
                    .Builder("МТС").Tariff(new Tariff("Light", 0.05m))
                .NumberParams(new PhoneNumberParameters("375", "29"))
                .BillingSystem(new BillingSystem()).Build();

            Subscriber subscriber1 = company.Subscribe(new SubscriberFactory(user1));
            Subscriber subscriber2 = company.Subscribe(new SubscriberFactory(user2));

            TerminalView terminal1View = new TerminalView(subscriber1.Terminal);
            TerminalView terminal2View = new TerminalView(subscriber2.Terminal);

            AutomaticTelephoneExchange ate = new AutomaticTelephoneExchange(company, 256);

            subscriber1.Terminal.ConnectTo(ate);
            subscriber2.Terminal.ConnectTo(ate);

            subscriber1.Terminal.CallTo(subscriber2.Terminal.Number);
            //terminal2.CallTo(terminal1.Number);
        }
    }
}