using ATE.Core.Interfaces;
using ATE.Entities.Users;

namespace ATE.Entities
{
    public class Contract : IContract
    {
        public string PhoneNumber { get; }
        public Tariff Tariff { get; }
        public User User { get; }

        public Contract(string phoneNumber, Tariff tariff, User user)
        {
            PhoneNumber = phoneNumber;
            Tariff = tariff;
            User = user;
        }
        
    }
}
