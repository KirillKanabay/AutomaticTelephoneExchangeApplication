using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;
using ATE.Core.Enums;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
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
            var billingAccount = new BillingAccount(contract);
            _billingAccounts.Add(billingAccount);

            return billingAccount;
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
                //todo: прервать звонок
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
        
        private IBillingAccount FindBillingAccount(string phoneNumber)
        {
            return _billingAccounts.FirstOrDefault(a => a.Contract.PhoneNumber == phoneNumber);
        }
    }
}