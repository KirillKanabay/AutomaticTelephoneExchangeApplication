using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;

namespace ATE.Core.Factories
{
    public abstract class AbstractSubscriberFactory
    {
        public abstract IBillingAccount CreateBillingAccount();
        public abstract Contract CreateContract(Company company);
        public abstract BaseTerminal CreateTerminal(Contract contract);
    }
}