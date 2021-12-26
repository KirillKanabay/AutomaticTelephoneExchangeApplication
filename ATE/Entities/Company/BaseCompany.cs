using System.Collections.Generic;
using ATE.Core.Interfaces;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Factories;
using ATE.Interfaces.ATE;

namespace ATE.Entities.Company
{
    public abstract class BaseCompany
    {
        public string Name { get; protected set; }
        protected ICollection<IContract> Contracts;
        protected ICollection<BaseStation> Stations;
        public PhoneNumberParameters NumberParams { get; protected set; }
        public BillingSystem BillingSystem { get; protected set; }
        public Tariff Tariff { get; protected set; }
        public abstract Subscriber Subscribe(AbstractSubscriberFactory subscriberFactory);
        public abstract void AddStation(BaseStation station);
    }
}
