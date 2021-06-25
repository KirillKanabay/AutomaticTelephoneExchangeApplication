namespace ATE.Core.Entities
{
    public class Tariff
    {
        public string Name { get; }
        public decimal PricePerMinuteCall { get; }
        
        public Tariff(string name, decimal pricePerMinuteCall)
        {
            Name = name;
            PricePerMinuteCall = pricePerMinuteCall;
        }
        
        public override string ToString()
        {
            return $"#{Name} Стоимость одной минуты звонка:{PricePerMinuteCall}";
        }
    }
}
