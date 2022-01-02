using ATE.Abstractions.Domain.Company;

namespace ATE.Domain.Company.Tariff
{
    public class EasySayTariff : BaseTariff
    {
        public EasySayTariff()
        {
            Name = "Легко сказать";
            PricePerMinuteCall = 0.05m;
        }
    }
}
