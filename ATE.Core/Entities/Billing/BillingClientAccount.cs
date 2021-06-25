namespace ATE.Core.Entities.Billing
{
    public class BillingUserAccount : BillingAccount
    {
        private readonly User _user;
        public BillingUserAccount(User user)
        {
            _user = user;
        }
    }
}