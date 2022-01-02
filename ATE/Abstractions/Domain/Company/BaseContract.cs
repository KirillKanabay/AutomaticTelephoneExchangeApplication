using ATE.Domain.Company.Tariff;

namespace ATE.Abstractions.Domain.Company
{
    public abstract class BaseContract
    {
        public string PhoneNumber { get; set; }
        public BaseTariff Tariff { get; set; }
    }
}
