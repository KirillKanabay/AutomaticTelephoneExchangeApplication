namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingAccount
    {
        decimal Balance { get; }
        void Deposit(decimal money);
        void WriteOff(decimal money);
    }
}