using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Args;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Core.Interfaces.Billings;
using ATE.Entities.Company;
using ATE.Enums;

namespace ATE.Entities.Billings
{
    public class BillingSystem : IBillingSystem
    {
        private readonly ICollection<IBillingAccount> _billingAccounts;
        
        public ICollection<CallInformation> Calls { get; }
        
        public BillingSystem()
        {
            _billingAccounts = new List<IBillingAccount>();
            Calls = new List<CallInformation>();
        }
        
        public IBillingAccount Register(IContract contract)
        {
            throw new NotImplementedException();
        }
        
        public void SubscribeToTerminal(ITerminal terminal)
        {
            terminal.CallEvent += OnTerminalCall;
            terminal.CallEndedEvent += OnTerminalCallEnded;
        }

        public void UnsubscribeFromTerminal(ITerminal terminal)
        {
            terminal.CallEvent -= OnTerminalCall;
            terminal.CallEndedEvent -= OnTerminalCallEnded;

        }

        private void OnTerminalCall(object sender, CallArgs e)
        {
            var acc = _billingAccounts.FirstOrDefault(a => a.Contract.PhoneNumber == e.FromNumber);
            if (acc?.Balance < acc?.Tariff.PricePerMinuteCall)
            {
                throw new ArgumentException("Недостаточно средств для совершения вызова");
            }
            
        }
        
        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            var fromAcc = FindBillingAccount(e.FromNumber);
            var targetAcc = FindBillingAccount(e.TargetNumber);

            if (fromAcc == null || targetAcc == null)
            {
                throw new Exception("Не обнаружен аккаунт абонента вызова");
            }
            
            var callFromNumber = new CallInformation(e.Call, fromAcc.Tariff.PricePerMinuteCall , CallType.Outgoing);
            var callTargetNumber = new CallInformation(e.Call, targetAcc.Tariff.PricePerMinuteCall, CallType.Incoming);

            Calls.Add(callFromNumber);
            Calls.Add(callTargetNumber);
            
            fromAcc.WriteOff(callFromNumber.Price);
            fromAcc.WriteOff(callTargetNumber.Price);
        }

        public void Deposit(Client client, decimal money)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            if (money <= 0)
            {
                throw new ArgumentException("Сумма пополнения счета не может быть меньше или равно нулю");
            }

            client.Balance += money;
        }

        public void WriteOff(Client client, decimal money)
        {
            if (money < 0)
            {
                throw new ArgumentException("Сумма списания счета не может быть меньше нуля");
            }
            
            client.Balance -= money;
        }

        private IBillingAccount FindBillingAccount(string phoneNumber)
        {
            return _billingAccounts.FirstOrDefault(a => a.Contract.PhoneNumber == phoneNumber);
        }
    }
}