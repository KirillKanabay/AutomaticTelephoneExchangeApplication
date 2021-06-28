namespace ATE.Core.Entities
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
