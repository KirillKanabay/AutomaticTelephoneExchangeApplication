using System;
using ATE.Core.Entities.Users;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public class BillingAccount : IBillingAccount
    {
        public User User { get; }
        public IContract Contract { get; }
        public ITariff Tariff { get; }
        public decimal Balance { get; private set; }

        public BillingAccount(IContract contract)
        {
            if (contract == null)
            {
                throw new ArgumentException("Контракт не может быть равен Null");
            }
            Contract = contract;
            Tariff = Contract.Tariff;
            User = Contract.User;
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
            if (money < 0)
            {
                throw new ArgumentException("Сумма списания счета не может быть меньше нуля");
            }
            
            Balance -= money;
        }
    }
}