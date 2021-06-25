using System.Reflection.Metadata;
using ATE.Core.Entities.ATE;

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

        public Contract(string phoneNumber, Tariff tariff, Client client, Company company)
        {
            PhoneNumber = phoneNumber;

            Tariff = tariff;
            Client = client;
            Company = company;
            
            ClientId = client.Id;
            TariffId = tariff.Id;
            CompanyId = company.Id;
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
