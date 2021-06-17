namespace ATE.Core.Entities
{
    public class Contract : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public Client Client { get; set; }
        public Company Company { get; set; }
    }
}
