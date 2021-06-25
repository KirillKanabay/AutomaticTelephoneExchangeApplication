namespace ATE.Core.Entities.Billing
{
    public class BillingClientAccount : BillingAccount
    {
        private readonly Client _client;
        public BillingClientAccount(Client client)
        {
            _client = client;
        }
    }
}