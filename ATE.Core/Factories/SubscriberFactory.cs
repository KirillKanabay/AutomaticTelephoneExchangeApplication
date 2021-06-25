using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Billing;
using ATE.Core.Generators;
using ATE.Core.Interfaces;

namespace ATE.Core.Factories
{
    public class SubscriberFactory : AbstractSubscriberFactory
    {
        private readonly User _user;
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;
        public SubscriberFactory(User user)
        {
            _user = user;
            _phoneNumberGenerator = new PhoneNumberGenerator();
        }
        
        public override IBillingAccount CreateBillingAccount(ICompany company)
        {
            var billingAccount = company.BillingSystem.Register(_user);
            return billingAccount;
        }

        public override IContract CreateContract(ICompany company)
        {
            var number = _phoneNumberGenerator.Generate(company);
            var contract = new Contract(number, company.Tariff, _user);

            return contract;
        }

        public override BaseTerminal CreateTerminal(IContract contract)
        {
            var terminal = new Phone(contract.PhoneNumber);
            return terminal;
        }
    }
}