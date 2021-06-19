namespace ATE.Core.Entities
{
    public class Tariff : BaseEntity
    {
        public string Name { get; set; }
        public decimal PricePerCall { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
