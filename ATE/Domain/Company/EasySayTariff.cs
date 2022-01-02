using ATE.Abstractions.Domain.Company;

namespace ATE.Domain.Company
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
