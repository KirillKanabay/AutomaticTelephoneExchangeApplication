using ATE.Entities.Company;
using ATE.Entities.Users;

namespace ATE.Factories
{
    public abstract class AbstractClientFactory
    {
        public abstract Client CreateClient(User user, BaseCompany company);
    }
}
