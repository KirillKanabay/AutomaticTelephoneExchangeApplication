using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public List<Contract> Contracts { get; set; }

        public override string ToString()
        {
            return $"Название: {Name}. Количество оформленных контрактов: {Contracts?.Count ?? 0}";
        }
    }
}
