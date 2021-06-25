using System.Collections.Generic;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Billing;
using ATE.Core.Factories;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Builders;

namespace ATE.Core.Entities
{
    public class Company
    {
        public string Name { get; private set; }
        public BillingSystem BillingSystem { get; private set; }
        public PhoneNumberParameters NumberParams { get; private set; }
        public ICollection<Contract> Contracts { get; private set; }
        public Tariff Tariff { get; private set; }

        private Company(string companyName)
        {
            Name = companyName;
            Contracts = new List<Contract>();
        }

        public Company(string companyName, PhoneNumberParameters numberParams, BillingSystem billingSystem,
            Tariff tariff) : this(companyName)
        {
            BillingSystem = billingSystem;
            Tariff = tariff;
            NumberParams = numberParams;
        }

        public Subscriber Subscribe(AbstractSubscriberFactory factory)
        {
            IBillingAccount account = factory.CreateBillingAccount();
            Contract contract = factory.CreateContract(this);
            BaseTerminal terminal = factory.CreateTerminal(contract);
            var subscriber = new Subscriber(terminal, contract, account);
            return subscriber;
        }

        public class Builder : ICompanyBuilder
        {
            private readonly Company _company;

            public Builder(string name)
            {
                _company = new Company(name);
            }

            public ICompanyBuilder NumberParams(PhoneNumberParameters numberParameters)
            {
                _company.NumberParams = numberParameters;
                return this;
            }

            public ICompanyBuilder BillingSystem(BillingSystem billingSystem)
            {
                _company.BillingSystem = billingSystem;
                return this;
            }

            public ICompanyBuilder Tariff(Tariff tariff)
            {
                _company.Tariff = tariff;
                return this;
            }

            public Company Build() => _company;
        }
    }
}