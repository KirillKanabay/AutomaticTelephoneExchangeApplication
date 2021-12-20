namespace ATE.Core.Entities.Users
{
    public class User
    {
        public string FirstName { get; }
        public string SecondName { get; }
        
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
