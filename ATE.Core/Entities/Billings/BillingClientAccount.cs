using ATE.Core.Entities.Users;

namespace ATE.Core.Entities.Billings
{
    public class BillingUserAccount : BaseBillingAccount
    {
        private readonly User _user;
        public BillingUserAccount(User user)
        {
            _user = user;
        }
    }
}