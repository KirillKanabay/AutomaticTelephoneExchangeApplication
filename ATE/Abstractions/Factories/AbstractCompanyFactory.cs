using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractCompanyFactory
    {
        public abstract BaseCompany CreateCompany();
    }
}
