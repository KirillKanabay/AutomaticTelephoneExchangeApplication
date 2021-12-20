using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;

namespace ATE.Factories
{
    public abstract class AbstractSubscriberFactory
    {
        public abstract IContract CreateContract(ICompany company);
        public abstract IBillingAccount CreateBillingAccount(ICompany company, IContract contract);
        public abstract ITerminal CreateTerminal(IContract contract);
    }
}