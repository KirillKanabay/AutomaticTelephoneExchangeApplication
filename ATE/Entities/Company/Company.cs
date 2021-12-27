using System.Collections.Generic;
using System.Linq;
using ATE.Entities.ATE;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Factories;

namespace ATE.Entities.Company
{
    public class Company : BaseCompany
    {
        private readonly AbstractClientFactory _clientFactory;

        public Company(BillingSystem billingSystem,
            AbstractClientFactory clientFactory)
        {
            BillingSystem = billingSystem;
            _clientFactory = clientFactory;

            Clients = new List<Client>();
            Stations = new List<BaseStation>();
        }
        
        public override Client RegisterClient(User user)
        {
            var client = _clientFactory.CreateClient(user, this);
            Clients.Add(client);

            return client;
        }
        
        public override void AddStation(BaseStation station)
        {
            Stations.Add(station);
        }

        public override bool PhoneNumberExists(string phoneNumber)
        {
            return Clients.Any(c => c.Contract.PhoneNumber.Equals(phoneNumber));
        }
    }
}