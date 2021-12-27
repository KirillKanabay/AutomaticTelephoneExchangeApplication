using ATE.Abstractions.Factories;
using ATE.Constants;
using ATE.Entities;
using ATE.Entities.Billings;
using ATE.Entities.Company;

namespace ATE.Factories
{
    public class DefaultCompanyFactory : AbstractCompanyFactory
    {
        private readonly AbstractClientFactory _clientFactory;

        public DefaultCompanyFactory(AbstractClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public override BaseCompany CreateCompany()
        {
            return new Company(new BillingSystem(), _clientFactory)
            {
                Name = DataConstants.DefaultCompanyName,
                PhoneNumberOptions = new PhoneNumberOptions(DataConstants.DefaultPhoneOptionsCountryCode,
                    DataConstants.DefaultPhoneOptionsCompanyCode),
            };
        }
    }
}