namespace ATE.Entities.Company.Tariff
{
    public class BaseTariff
    {
        public string Name { get; protected set; }
        public decimal PricePerMinuteCall { get; set; }
    }
}
