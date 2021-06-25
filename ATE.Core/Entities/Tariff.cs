namespace ATE.Core.Entities
{
    public class Tariff : BaseEntity
    {
        public string Name { get; }
        public decimal PricePerMinuteCall { get; }
        public Company Company { get; }
        
        public Tariff(string name, decimal pricePerMinuteCall, Company company)
        {
            Name = name;
            PricePerMinuteCall = pricePerMinuteCall;
            Company = company;
        }
        
        public override string ToString()
        {
            return $"#{Id} {Name} Стоимость одной минуты звонка:{PricePerMinuteCall}";
        }
    }
}
