using ATE.Core.Entities;

namespace ATE.Core.Specifications.Contracts
{
    public class ContractByPhoneNumberSpec : BaseSpecification<Contract>
    {
        public ContractByPhoneNumberSpec(string phoneNumber) : base(c => c.PhoneNumber == phoneNumber)
        {
            AddInclude(c => c.Client);
            AddInclude(c => c.Company);
            AddInclude(c => c.Tariff);
        }
    }
}