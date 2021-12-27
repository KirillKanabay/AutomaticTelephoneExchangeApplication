using ATE.Entities.Company;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractCompanyFactory
    {
        public abstract BaseCompany CreateCompany();
    }
}
