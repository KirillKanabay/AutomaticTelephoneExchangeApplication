using System.Reflection.Metadata;
using ATE.Core.Entities.ATE;

namespace ATE.Core.Entities
{
    public class Contract
    {
        public string PhoneNumber { get; }
        
        public Tariff Tariff { get; }
        public User User { get; }
        public Company Company { get; }

        public Contract(string phoneNumber, Tariff tariff, User user)
        {
            PhoneNumber = phoneNumber;

            Tariff = tariff;
            User = user;
        }
        
    }
}
