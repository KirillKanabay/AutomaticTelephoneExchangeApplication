using ATE.Core.Entities;
using ATE.Core.Entities.Billings;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Builders;

namespace ATE.Core.Builders
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