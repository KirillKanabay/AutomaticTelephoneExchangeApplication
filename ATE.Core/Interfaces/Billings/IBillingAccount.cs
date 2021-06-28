using ATE.Core.Entities;
using ATE.Core.Entities.Users;

namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingAccount
    {
        User User { get; }
        IContract Contract { get; }
        decimal Balance { get; }
        void Deposit(decimal money);
        void WriteOff(decimal money);
    }
}