using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Builders;
using ATE.Entities;
using ATE.Entities.Billings;

namespace ATE.Builders
{
    public class DefaultCompanyDirector : ICompanyDirector
    {
        private readonly ICompanyBuilder _builder;
        public DefaultCompanyDirector(ICompanyBuilder builder)
        {
            _builder = builder;
        }
        public ICompany Make() =>_builder.Tariff(new Tariff("Light", 0.05m))
            .NumberParams(new PhoneNumberParameters("375", "29"))
            .BillingSystem(new BillingSystem()).AddAte(16).Build();
    }
}