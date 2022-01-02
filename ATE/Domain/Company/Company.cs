using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Domain.Station;
using ATE.Abstractions.Factories;
using ATE.Domain.Station;
using ATE.Domain.Users;
using ATE.Factories;

namespace ATE.Domain.Company
{
    public class Company : BaseCompany
    {
        private readonly AbstractClientFactory _clientFactory;

        public Company(AbstractClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            Clients = new List<Client>();
            StationCollection = new List<BaseStation>();
        }
        
        public override Client RegisterClient(User user)
        {
            var client = _clientFactory.CreateClient(user, this);
            Clients.Add(client);

            return client;
        }
        
        public override void AddStation(BaseStation station)
        {
            if (station == null)
            {
                throw new ArgumentNullException(nameof(station), "Station cannot be null");
            }

            StationCollection.Add(station);
            BillingSystem.SubscribeToStation(station);
            station.SubscribeToBillingSystem(BillingSystem);
        }

        public override bool PhoneNumberExists(string phoneNumber)
        {
            return Clients.Any(c => c.Contract.PhoneNumber.Equals(phoneNumber));
        }

        public override Client GetClientByPhoneNumber(string phoneNumber)
        {
            return Clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }
    }
}