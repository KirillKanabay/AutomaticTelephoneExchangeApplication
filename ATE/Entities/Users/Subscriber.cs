using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;

namespace ATE.Entities.Users
{
    public class Subscriber
    {
        public ITerminal Terminal { get; }
        public IContract Contract { get; }
        public IBillingAccount BillingAccount { get; }

        public Subscriber(ITerminal terminal, IContract contract, IBillingAccount billingAccount)
        {
            Terminal = terminal;
            Contract = contract;
            BillingAccount = billingAccount;
        }
    }
}