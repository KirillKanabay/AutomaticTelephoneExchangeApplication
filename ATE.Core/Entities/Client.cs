using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public virtual List<Contract> Contracts { get; }

        public Client(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
        
        public override string ToString()
        {
            return $"#{Id} {FirstName} {SecondName}";
        }
    }
}
