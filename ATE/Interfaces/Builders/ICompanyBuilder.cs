using ATE.Core.Entities;
using ATE.Entities;
using ATE.Entities.Billings;

namespace ATE.Core.Interfaces.Builders
{
    public interface ICompanyBuilder
    {
        ICompanyBuilder Tariff(Tariff tariff);
        ICompanyBuilder BillingSystem(BillingSystem billingSystem);
        ICompanyBuilder NumberParams(PhoneNumberParameters phoneNumberParameters);
        ICompanyBuilder AddAte(int portsCount);
        
        Company Build();
    }
}