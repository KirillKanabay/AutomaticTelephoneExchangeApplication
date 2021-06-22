namespace ATE.Core.Entities
{
    public class Tariff : BaseEntity
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
            return $"#{Id} {Name} Стоимость одной минуты звонка:{PricePerMinuteCall}";
        }
    }
}
