using System;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;
using ATE.Core.Enums;

namespace ATE.Core.Entities.Billings
{
    public class CallInformation
    {
        #region Props

        public BaseTerminal Terminal { get; }
        
        public CallStatus Status { get; private set; }
        public Call Call { get; private set; }
        
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public TimeSpan? Duration => EndTime - StartTime;
        
        public decimal PricePerCall { get; private set; }
        public decimal Price => GetPrice();
        
        #endregion
       
        public CallInformation(BaseTerminal terminal, Call call)
        {
            Status = CallStatus.Waiting;
            Terminal = terminal;
            Call = Call;
            PricePerCall = terminal.Contract.Tariff.PricePerMinuteCall;
            
            SubscribeToTerminal();
        }

        private void SubscribeToTerminal()
        {
            Terminal.CallAcceptedEvent += OnTerminalCallingAccepted;
            Terminal.CallEndedEvent += OnTerminalCallingEnded;
            Terminal.CallRejectedEvent += OnTerminalCallingRejected;
        }

        private void UnsubscribeFromTerminal()
        {
            Terminal.CallAcceptedEvent -= OnTerminalCallingAccepted;
            Terminal.CallEndedEvent -= OnTerminalCallingEnded;
            Terminal.CallRejectedEvent -= OnTerminalCallingRejected;
        }
        
        private void OnTerminalCallingAccepted(object sender, CallArgs e)
        {
            Status = CallStatus.Accepted;
            StartTime = DateTime.UtcNow;
        }

        private void OnTerminalCallingEnded(object sender, CallArgs e)
        {
            EndTime = DateTime.Now;
            UnsubscribeFromTerminal();
        }

        private void OnTerminalCallingRejected(object sender, CallArgs e)
        {
            Status = CallStatus.Rejected;
            StartTime = DateTime.Now;
            EndTime = null;
            
            UnsubscribeFromTerminal();
        }

        private decimal GetPrice()
        {
            var durationInMinutes = Convert.ToDecimal(Duration?.TotalMinutes ?? 0);
            var price = Math.Ceiling(durationInMinutes) * PricePerCall;

            return price;
        }
    }
}