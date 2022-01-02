using System;
using ATE.Abstractions.Domain.Billings;
using ATE.Abstractions.Domain.Port;
using ATE.Abstractions.Domain.Terminal;
using ATE.Args;
using ATE.Domain.Terminal;

namespace ATE.Abstractions.Domain.Station
{
    public abstract class BaseStation
    {
        public event EventHandler<CallArgs> CallStartedEvent; 
        public event EventHandler<CallArgs> CallEndedEvent;
        public abstract BasePort ConnectTerminal(BaseTerminal terminal);
        public abstract void SubscribeToBillingSystem(BaseBillingSystem billingSystem);
        public abstract void OnTerminalStartedCall(object sender, CallArgs e);
        public abstract void OnTerminalAcceptedCall(object sender, CallArgs e);
        public abstract void OnTerminalRejectedCall(object sender, CallArgs e);
        public abstract void OnTerminalEndedCall(object sender, CallArgs e);
        public abstract void OnCallAllowed(object sender, CallArgs e);
        public abstract void OnCallCanceled(object sender, CallCanceledArgs e);

        protected virtual void OnCallEndedEvent(object sender, CallArgs e)
        {
            CallEndedEvent?.Invoke(sender, e);
        }
        protected virtual void OnCallStartedEvent(object sender, CallArgs e)
        {
            CallStartedEvent?.Invoke(sender, e);
        }
    }
}
