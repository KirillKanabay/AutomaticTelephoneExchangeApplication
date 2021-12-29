using System.Collections.Generic;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Users;

namespace ATE.Entities.Company
{
    public abstract class BaseCompany
    {
        protected ICollection<Client> Clients;
        protected ICollection<BaseStation> Stations;
        public string Name { get; set; }
        public PhoneNumberOptions PhoneNumberOptions { get; set; }
        public BaseBillingSystem BillingSystem { get; set; }
        public abstract Client RegisterClient(User user);
        public abstract void AddStation(BaseStation station);
        public abstract bool PhoneNumberExists(string phoneNumber);
        public abstract Client GetClientByPhoneNumber(string phoneNumber);
    }
}
