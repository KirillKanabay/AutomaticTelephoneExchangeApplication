using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;
using ATE.Core.Entities.Users;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public class BillingSystem : IBillingSystem
    {
        private readonly ICollection<IBillingAccount> _billingAccounts;
        private readonly ICollection<CallInformation> _calls;

        public BillingSystem()
        {
            _billingAccounts = new List<IBillingAccount>();
            _calls = new List<CallInformation>();
        }
        
        public IBillingAccount Register(User user)
        {
            var billingAccount = new BillingAccount(user);
            _billingAccounts.Add(billingAccount);

            return billingAccount;
        }

        public IEnumerable<CallInformation> GetCalls(string number)
        {
            return _calls.Where(c => c.Call.FromNumber == number || c.Call.TargetNumber == number);
        }
        
        public void SubscribeToTerminal(BaseTerminal terminal)
        {
            terminal.CallEvent += OnTerminalCallEvent;
        }

        public void UnsubscribeFromTerminal(BaseTerminal terminal)
        {
            terminal.CallEvent -= OnTerminalCallEvent;
        }

        private void OnTerminalCallEvent(object sender, CallArgs e)
        {
            BaseTerminal terminal = (BaseTerminal) sender;
            var billingUserAccount = _billingAccounts.FirstOrDefault(acc => acc.User == terminal.Contract.User);
            //todo: bua check for null
            
            //todo: уменьшить цепочку вызовов
            if (billingUserAccount?.Balance < terminal.Contract.Tariff.PricePerMinuteCall)
            {
                throw new ArgumentException("Недостаточно средств для совершения операции");
            }
           
            var call = new CallInformation((BaseTerminal) sender, e.Call);

            _calls.Add(call);
        }

        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            BaseTerminal terminal = (BaseTerminal) sender;
            var billingUserAccount = _billingAccounts.FirstOrDefault(acc => acc.User == terminal.Contract.User);
            var call = _calls.FirstOrDefault(c => c.Call.Id == e.Call.Id);
            
            billingUserAccount.WriteOff(call.Price);
        }
    }
}