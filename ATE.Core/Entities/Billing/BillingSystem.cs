using System.Collections.Generic;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities.Billing
{
    public class BillingSystem
    {
        private readonly ICollection<IBillingAccount> BillingAccounts;

        public BillingSystem()
        {
            BillingAccounts = new List<IBillingAccount>();
        }

        public BillingAccount Register(Company company)
        {
            var billingAccount = new BillingCompanyAccount(company);
            BillingAccounts.Add(billingAccount);

            return billingAccount;
        }

        public BillingAccount Register(User client)
        {
            var billingAccount = new BillingUserAccount(client);
            BillingAccounts.Add(billingAccount);

            return billingAccount;
        }
    }
}