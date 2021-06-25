using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Subscriber
    {
        public BaseTerminal Terminal { get; }
        public Contract Contract { get; }
        public IBillingAccount BillingAccount { get; }

        public Subscriber(BaseTerminal terminal, Contract contract, IBillingAccount billingAccount)
        {
            Terminal = terminal;
            Contract = contract;
            BillingAccount = billingAccount;
        }
    }
}