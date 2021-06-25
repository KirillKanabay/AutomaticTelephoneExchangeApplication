namespace ATE.Core.Entities.Billings
{
    public class BillingCompanyAccount : BaseBillingAccount
    {
        public Company Company { get; }

        public BillingCompanyAccount(Company company)
        {
            Company = company;
        }
    }
}