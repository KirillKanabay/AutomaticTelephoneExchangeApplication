using ATE.Entities.Company.Tariff;
using ATE.Entities.Users;

namespace ATE.Entities.Company.Contracts
{
    public abstract class BaseContract
    {
        public string PhoneNumber { get; set; }
        public BaseTariff Tariff { get; set; }
    }
}
