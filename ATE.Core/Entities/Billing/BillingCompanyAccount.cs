namespace ATE.Core.Entities.Billing
{
    public class BillingCompanyAccount : BillingAccount
    {
        public Company Company { get; }

        public BillingCompanyAccount(Company company)
        {
            Company = company;
        }
    }
}