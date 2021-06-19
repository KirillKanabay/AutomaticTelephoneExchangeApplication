using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public List<Contract> Contracts { get; set; }
        
        public List<Tariff> Tariffs { get; set; }
        public override string ToString()
        {
            return $"#{Id} Название: {Name}. Код страны: {CountryCode}. Код компании: {CompanyCode}";
        }
    }
}
