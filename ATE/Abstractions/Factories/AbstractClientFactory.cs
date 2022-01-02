using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;
using ATE.Domain.Users;
using ATE.Entities.Users;

namespace ATE.Factories
{
    public abstract class AbstractClientFactory
    {
        public abstract Client CreateClient(User user, BaseCompany company);
    }
}
