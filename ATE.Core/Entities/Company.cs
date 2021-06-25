using System.Collections.Generic;
using System.Reflection.Metadata;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Billing;
using ATE.Core.Generators;

namespace ATE.Core.Entities
{
    public class Company : BaseEntity
    {
        public BillingSystem BillingSystem { get; }
        public PhoneNumberParameters NumberParams { get; }
        public List<Contract> Contracts { get; }
        
        public Tariff Tariff { get; }

        public Company(PhoneNumberParameters numberParams, BillingSystem billingSystem, Tariff tariff)
        {
            BillingSystem = billingSystem;
            Tariff = tariff;
            NumberParams = numberParams;
        }
        
        public Contract CreateContract(Client client, Tariff tariff)
        {
            var phoneNumber = new PhoneNumberGenerator().Generate(company: this);
            var contract = new Contract(phoneNumber, tariff, client, this);
            Contracts.Add(contract);

            return contract;
        }
    }
}
