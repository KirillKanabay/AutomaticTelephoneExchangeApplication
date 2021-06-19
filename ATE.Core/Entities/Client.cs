using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<Contract> Contracts { get; set; }

        public override string ToString()
        {
            return $"#{Id} {FirstName} {SecondName}";
        }
    }
}
