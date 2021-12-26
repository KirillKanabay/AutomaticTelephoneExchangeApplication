using System.Collections.Generic;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;
using ATE.Core.Interfaces.Builders;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Factories;

namespace ATE.Entities.Company
{
    public class Company : BaseCompany
    {
        public Company(string companyName, BillingSystem billingSystem, PhoneNumberParameters numberParameters)
        {
            Name = companyName;
            BillingSystem = billingSystem;
            NumberParams = numberParameters;

            Contracts = new List<IContract>();
            Stations = new List<BaseStation>();
        }
        
        public override Subscriber Subscribe(AbstractSubscriberFactory factory)
        {
            IContract contract = factory.CreateContract(this);
            IBillingAccount account = factory.CreateBillingAccount(this, contract);
            ITerminal terminal = factory.CreateTerminal(contract);
            
            var subscriber = new Subscriber(terminal, contract, account);
            BillingSystem.SubscribeToTerminal(terminal);
            
            Contracts.Add(contract);
            
            return subscriber;
        }

        public override void AddStation(BaseStation station)
        {
            Stations.Add(station);
        }
    }
}