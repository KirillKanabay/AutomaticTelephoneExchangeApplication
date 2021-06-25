namespace ATE.Core.Interfaces
{
    public interface IBillingAccount
    {
        public decimal Balance { get; }
        public void Deposit(decimal money);
        public void WriteOff(decimal money);
    }
}