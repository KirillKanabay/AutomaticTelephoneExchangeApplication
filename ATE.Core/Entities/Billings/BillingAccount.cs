using System;
using ATE.Core.Entities.Users;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public class BillingAccount : IBillingAccount
    {
        public User User { get; }
        public decimal Balance { get; private set; }

        public BillingAccount(User user)
        {
            User = user;
        }
        
        public void Deposit(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Сумма пополнения счета не может быть меньше или равно нулю");
            }
            Balance += money;
        }

        public void WriteOff(decimal money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Сумма списания счета не может быть меньше или равно нулю");
            }
            
            Balance -= money;
        }
    }
}