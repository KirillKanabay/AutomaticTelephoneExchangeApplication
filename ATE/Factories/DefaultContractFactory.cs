using ATE.Entities.Company;
using ATE.Entities.Company.Contracts;
using ATE.Entities.Company.Tariff;
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
