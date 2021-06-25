using System.Collections.Generic;
using ATE.Core.Entities.Users;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public class BillingSystem : IBillingSystem//todo: Добавить интерфейс
    {
        private readonly ICollection<IBillingAccount> BillingAccounts;

        public BillingSystem()
        {
            BillingAccounts = new List<IBillingAccount>();
        }

        public IBillingAccount Register(Company company)
        {
            var billingAccount = new BillingCompanyAccount(company);
            BillingAccounts.Add(billingAccount);

            return billingAccount;
        }

        public IBillingAccount Register(User client)
        {
            var billingAccount = new BillingUserAccount(client);
            BillingAccounts.Add(billingAccount);

            return billingAccount;
        }
    }
}