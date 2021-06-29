using System.Collections.Generic;
using ATE.Core.Entities;
using ATE.Core.Entities.Billings;
using ATE.Core.Factories;
using ATE.Core.Interfaces.ATE;

namespace ATE.Core.Interfaces
{
    public interface ICompany
    {
        ICollection<IContract> Contracts { get; }
        ICollection<IAutomaticTelephoneExchange> AteCollection { get; }
        PhoneNumberParameters NumberParams { get; }
        BillingSystem BillingSystem { get; }
        Tariff Tariff { get; }
        Subscriber Subscribe(AbstractSubscriberFactory subscriberFactory);
        void AddAte(IAutomaticTelephoneExchange ate);
    }
}