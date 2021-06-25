using System;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities.Billing
{
    public abstract class BillingAccount : IBillingAccount
    {
        public decimal Balance { get; private set; }
        
        public virtual void Deposit(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Сумма пополнения счета не может быть меньше или равно нулю");
            }
            Balance += money;
        }

        public virtual void WriteOff(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Сумма списания счета не может быть меньше или равно нулю");
            }

            if (Balance - money < 0)
            {
                throw new Exception("Недостаточно средств на балансе");
            }

            Balance += money;
        }
    }
}