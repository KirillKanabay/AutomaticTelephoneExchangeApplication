using ATE.Entities.Company;
using ATE.Entities.Company.Contracts;

namespace ATE.Factories
{
    public abstract class AbstractContractFactory
    {
        public abstract BaseContract CreateContract(BaseCompany company);
    }
}
