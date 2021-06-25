using System.Collections.Generic;

namespace ATE.Core.Entities
{
    public class User
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }

        public User(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
        
        public override string ToString()
        {
            return $"#{FirstName} {SecondName}";
        }
    }
}
