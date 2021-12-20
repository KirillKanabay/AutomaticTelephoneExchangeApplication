using System.Collections.Generic;
using ATE.Core.Entities;
using ATE.Core.Interfaces.ATE;
using ATE.Entities;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Factories;
using ATE.Interfaces.ATE;

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