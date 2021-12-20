using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Users;
using ATE.Core.Generators;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;

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

        public SubscriberFactory(User user, IPhoneNumberGenerator generator)
        {
            _user = user;
            _phoneNumberGenerator = generator;
        }
        
        public override IBillingAccount CreateBillingAccount(ICompany company, IContract contract)
        {
            var billingAccount = company.BillingSystem.Register(contract);
            return billingAccount;
        }

        public override IContract CreateContract(ICompany company)
        {
            var number = _phoneNumberGenerator.Generate(company);
            var contract = new Contract(number, company.Tariff, _user);

            return contract;
        }

        public override ITerminal CreateTerminal(IContract contract)
        {
            var terminal = new Terminal(contract);
            return terminal;
        }
    }
}