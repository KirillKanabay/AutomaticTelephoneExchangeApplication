using ATE.Core.Interfaces;

namespace ATE.Core.Entities
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
