using ATE.Core.Entities;
using ATE.Entities.Users;

namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingAccount
    {
        User User { get; }
        IContract Contract { get; }
        ITariff Tariff { get; }
        decimal Balance { get; }
        void Deposit(decimal money);
        void WriteOff(decimal money);
    }
}