using System.Collections.Generic;
using ATE.Abstractions.Domain.Billings;
using ATE.Abstractions.Domain.Station;
using ATE.Domain;
using ATE.Domain.Company;
using ATE.Domain.Users;

namespace ATE.Abstractions.Domain.Company
{
    public abstract class BaseCompany
    {
        protected ICollection<Client> Clients;
        protected ICollection<BaseStation> StationCollection;
        public IEnumerable<BaseStation> Stations => StationCollection;
        public string Name { get; set; }
        public PhoneNumberOptions PhoneNumberOptions { get; set; }
        public BaseBillingSystem BillingSystem { get; set; }
        
        public abstract Client RegisterClient(User user);
        public abstract void AddStation(BaseStation station);
        public abstract bool PhoneNumberExists(string phoneNumber);
        public abstract Client GetClientByPhoneNumber(string phoneNumber);
    }
}
