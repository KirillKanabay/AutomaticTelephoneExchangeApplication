using ATE.Core.Entities.Billings;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Users
{
    public class User
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        
        public IBillingAccount BillingAccount { get; private set; }
        
        public User(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public void RegisterToBillingSystem(IBillingSystem bs)
        {
            BillingAccount = bs.Register(this);
        }
        
        public override string ToString()
        {
            return $"#{FirstName} {SecondName}";
        }
    }
}
