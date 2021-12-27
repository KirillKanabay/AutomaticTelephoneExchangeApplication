using System.Collections.Generic;
using ATE.Core.Interfaces;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;

namespace ATE.Entities.Company
{
    public abstract class BaseCompany
    {
        protected ICollection<IContract> Contracts;
        protected ICollection<BaseStation> Stations;
        public string Name { get; protected set; }
        public PhoneNumberParameters NumberParams { get; protected set; }
        public BillingSystem BillingSystem { get; protected set; }
        public abstract Client RegisterClient(User user, BaseTariff tariff);
        public abstract void AddStation(BaseStation station);
    }
}
