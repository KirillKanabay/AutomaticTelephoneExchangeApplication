using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;
using ATE.Domain.Company.Contracts;
using ATE.Domain.Company.Tariff;
using ATE.Interfaces;

namespace ATE.Factories
{
    public class DefaultContractFactory : AbstractContractFactory
    {
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;
        public DefaultContractFactory(IPhoneNumberGenerator phoneNumberGenerator)
        {
            _phoneNumberGenerator = phoneNumberGenerator;
        }

        public override BaseContract CreateContract(BaseCompany company)
        {
            return new Contract()
            {
                PhoneNumber = _phoneNumberGenerator.Generate(company),
                Tariff = new EasySayTariff()
            };
        }
    }
}
