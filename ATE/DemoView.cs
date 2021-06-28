using ATE.Core.Builders;
using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Users;
using ATE.Core.Factories;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Builders;

namespace ATE
{
    public class DemoView
    {
        public void Show()
        {
            User user1 = new User("Kirill", "Kanabay");
            User user2 = new User("Ivan", "Ivanov");

            ICompanyBuilder builder = new Company.Builder("MTC");
            ICompany company = new DefaultCompanyDirector(builder).Make();

            Subscriber subscriber1 = company.Subscribe(new SubscriberFactory(user1));
            Subscriber subscriber2 = company.Subscribe(new SubscriberFactory(user2));
            
            TerminalView terminal1View = new TerminalView(subscriber1.Terminal);
            TerminalView terminal2View = new TerminalView(subscriber2.Terminal);

            AutomaticTelephoneExchange ate = new AutomaticTelephoneExchange(company, 256);
            
            subscriber1.BillingAccount.Deposit(5);
            subscriber2.BillingAccount.Deposit(5);
            
            subscriber1.Terminal.ConnectTo(ate);
            subscriber2.Terminal.ConnectTo(ate);

            subscriber1.Terminal.CallTo(subscriber2.Terminal.Number);
            
            subscriber1.Terminal.EndCall();
            //terminal2.CallTo(terminal1.Number);
        }
    }
}