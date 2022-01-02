using ATE.Abstractions.Domain.Company;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractContractFactory
    {
        public abstract BaseContract CreateContract(BaseCompany company);
    }
}
