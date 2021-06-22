using System.Reflection.Metadata;

namespace ATE.Core.Entities
{
    public class Contract : BaseEntity
    {
        private Phone _phone;
        public string PhoneNumber { get; }
        public int ClientId { get; }
        public int TariffId { get; }
        public int CompanyId { get; }
        public virtual Tariff Tariff { get; }
        public virtual Client Client { get; }
        public virtual Company Company { get; }

        public Contract(string phoneNumber, int clientId, int tariffId, int companyId)
        {
            PhoneNumber = phoneNumber;
            ClientId = clientId;
            TariffId = tariffId;
            CompanyId = companyId;
        }
        
        public Phone Phone
        {
            get
            {
                return _phone ??= new Phone(this);
            }
        }
    }
}
