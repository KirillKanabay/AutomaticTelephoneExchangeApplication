using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;

namespace ATE.Core.Factories
{
    public abstract class AbstractSubscriberFactory
    {
        public abstract IBillingAccount CreateBillingAccount(ICompany company);
        public abstract IContract CreateContract(ICompany company);
        public abstract BaseTerminal CreateTerminal(IContract contract);
    }
}