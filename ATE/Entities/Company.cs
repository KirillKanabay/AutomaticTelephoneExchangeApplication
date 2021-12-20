using System.Collections.Generic;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Billings;
using ATE.Core.Factories;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;
using ATE.Core.Interfaces.Builders;

namespace ATE.Core.Entities
{
    public class Company : ICompany
    {
        public string Name { get; private set; }
        public BillingSystem BillingSystem { get; private set; }
        public PhoneNumberParameters NumberParams { get; private set; }
        public ICollection<IContract> Contracts { get; private set; }
        public ICollection<IAutomaticTelephoneExchange> AteCollection { get; private set; }
        public Tariff Tariff { get; private set; }

        private Company(string companyName)
        {
            Name = companyName;
            Contracts = new List<IContract>();
            AteCollection = new List<IAutomaticTelephoneExchange>();
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
            IContract contract = factory.CreateContract(this);
            IBillingAccount account = factory.CreateBillingAccount(this, contract);
            ITerminal terminal = factory.CreateTerminal(contract);
            
            var subscriber = new Subscriber(terminal, contract, account);
            BillingSystem.SubscribeToTerminal(terminal);
            
            Contracts.Add(contract);
            
            return subscriber;
        }

        public void AddAte(IAutomaticTelephoneExchange ate)
        {
            AteCollection.Add(ate);
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

            public ICompanyBuilder AddAte(int portsCount)
            {
                var ate = new AutomaticTelephoneExchange(_company, portsCount);
                _company.AddAte(ate);

                return this;
            }
            
            public Company Build() => _company;
        }
    }
}