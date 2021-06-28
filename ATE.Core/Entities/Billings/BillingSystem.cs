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
            if (acc?.Balance < acc?.Contract.Tariff.PricePerMinuteCall)
            {
                throw new ArgumentException("Недостаточно средств для совершения вызова");
                //todo: прервать звонок
            }
            
        }
        
        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            var acc = _billingAccounts.FirstOrDefault(a => a.Contract.PhoneNumber == e.FromNumber);

            double durationInMinutes = e.DurationInMinutes;
            decimal price = CalculateCallPrice(durationInMinutes, acc?.Contract.Tariff.PricePerMinuteCall ?? 0);

            var callFromNumber = new CallInformation
            {
                ClientPhoneNumber = e.FromNumber,
                DestinationPhoneNumber = e.TargetNumber,
                CallDate = e.Date,
                CallType = CallType.Outgoing,
                Duration = durationInMinutes,
                Price = price
            };
            
            var callTargetNumber = new CallInformation
            {
                ClientPhoneNumber = e.TargetNumber,
                DestinationPhoneNumber = e.FromNumber,
                CallDate = e.Date,
                CallType = CallType.Incoming,
                Duration = durationInMinutes,
                Price = 0
            };

            Calls.Add(callFromNumber);
            Calls.Add(callTargetNumber);
        }

        private decimal CalculateCallPrice(double durationInMinutes, decimal pricePerMinuteCall)
        {
            decimal price = Convert.ToDecimal(durationInMinutes) * pricePerMinuteCall;
            return price;
        }

        private void WriteOff(IBillingAccount acc, decimal price)
        {
            acc.WriteOff(price);
        }
        
    }
}