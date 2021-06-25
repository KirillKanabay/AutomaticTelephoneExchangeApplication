using ATE.Core.Entities;
using ATE.Core.Entities.Billings;

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