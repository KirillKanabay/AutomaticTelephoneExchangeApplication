using ATE.Core.Entities;

namespace ATE.Entities
{
    public class Tariff : ITariff
    {
        public string Name { get; }
        public decimal PricePerMinuteCall { get; }
        
        public Tariff(string name, decimal pricePerMinuteCall)
        {
            Name = name;
            PricePerMinuteCall = pricePerMinuteCall;
        }
    }
}
