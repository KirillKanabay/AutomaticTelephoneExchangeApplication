using System;
using System.Collections.Generic;
using ATE.Core.Interfaces;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;

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
        
        public override Client RegisterClient(User user, BaseTariff tariff)
        {
            throw new NotImplementedException();
        }

        public override void AddStation(BaseStation station)
        {
            Stations.Add(station);
        }
    }
}