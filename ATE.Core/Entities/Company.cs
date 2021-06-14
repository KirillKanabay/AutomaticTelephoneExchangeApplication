using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; }
        public IReadOnlyList<AutomaticTelephoneExchange> AteList { get; }
    }
}
