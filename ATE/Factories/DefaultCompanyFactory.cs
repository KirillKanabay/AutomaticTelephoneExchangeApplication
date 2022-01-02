using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Factories;
using ATE.Constants;
using ATE.Domain;
using ATE.Domain.Billings;
using ATE.Domain.Company;

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
            var company = new Company(_clientFactory)
            {
                Name = DataConstants.DefaultCompanyName,
                PhoneNumberOptions = new PhoneNumberOptions(DataConstants.DefaultPhoneOptionsCountryCode,
                    DataConstants.DefaultPhoneOptionsCompanyCode),
            };

            var billingSystem = new BillingSystem()
            {
                Company = company,
            };

            company.BillingSystem = billingSystem;

            return company;
        }
    }
}