using ATE.Core.Entities;
using ATE.Core.Entities.Billing;

namespace ATE.Core.Interfaces.Builders
{
    public interface ICompanyBuilder
    {
        ICompanyBuilder Tariff(Tariff tariff);
        ICompanyBuilder BillingSystem(BillingSystem billingSystem);
        ICompanyBuilder NumberParams(PhoneNumberParameters phoneNumberParameters);

        Company Build();
    }
}