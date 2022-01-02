using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;
using ATE.Domain.Company.Contracts;

namespace ATE.Factories
{
    public abstract class AbstractContractFactory
    {
        public abstract BaseContract CreateContract(BaseCompany company);
    }
}
