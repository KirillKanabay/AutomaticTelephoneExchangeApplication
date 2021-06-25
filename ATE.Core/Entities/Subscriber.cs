using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Subscriber
    {
        public BaseTerminal Terminal { get; }
        public IContract Contract { get; }
        public IBillingAccount BillingAccount { get; }

        public Subscriber(BaseTerminal terminal, IContract contract, IBillingAccount billingAccount)
        {
            Terminal = terminal;
            Contract = contract;
            BillingAccount = billingAccount;
        }
    }
}