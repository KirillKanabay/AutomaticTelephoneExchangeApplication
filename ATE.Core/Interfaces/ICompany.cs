using System.Collections.Generic;
using ATE.Core.Entities;
using ATE.Core.Entities.Billing;
using ATE.Core.Factories;

namespace ATE.Core.Interfaces
{
    public interface ICompany
    {
        ICollection<IContract> Contracts { get; }
        PhoneNumberParameters NumberParams { get; }
        BillingSystem BillingSystem { get; }
        Tariff Tariff { get; }
        Subscriber Subscribe(AbstractSubscriberFactory subscriberFactory);
    }
}