using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Factories
{
    public abstract class AbstractSubscriberFactory
    {
        public abstract IContract CreateContract(ICompany company);
        public abstract IBillingAccount CreateBillingAccount(ICompany company, IContract contract);
        public abstract ITerminal CreateTerminal(IContract contract);
    }
}