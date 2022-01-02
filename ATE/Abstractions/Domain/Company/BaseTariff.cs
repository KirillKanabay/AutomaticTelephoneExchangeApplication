namespace ATE.Abstractions.Domain.Company
{
    public class BaseTariff
    {
        public string Name { get; protected set; }
        public decimal PricePerMinuteCall { get; set; }
    }
}
