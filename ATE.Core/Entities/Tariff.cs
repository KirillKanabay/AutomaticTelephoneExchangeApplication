namespace ATE.Core.Entities
{
    public class Tariff : BaseEntity
    {
        public string Name { get; set; }
        public decimal PricePerMinuteCall { get; set; }

        public override string ToString()
        {
            return $"#{Id} {Name} Стоимость одной минуты звонка:{PricePerMinuteCall}";
        }
    }
}
