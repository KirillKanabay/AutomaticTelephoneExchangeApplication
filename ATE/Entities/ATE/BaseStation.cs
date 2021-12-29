using System;
using ATE.Args;
using ATE.Entities.Billings;
using ATE.Entities.Port;
using ATE.Entities.Terminal;

namespace ATE.Entities.ATE
{
    public abstract class BaseStation
    {
        public event EventHandler<CallArgs> CallStartedEvent; 
        public event EventHandler<CallArgs> CallEndedEvent;
        public abstract BasePort ConnectTerminal(BaseTerminal terminal);
        public abstract void SubscribeToBillingSystem(BaseBillingSystem billingSystem);
        public abstract void OnTerminalStartingCall(object sender, CallArgs e);
        public abstract void OnTerminalAcceptingCall(object sender, CallArgs e);
        public abstract void OnTerminalRejectingCall(object sender, CallArgs e);
        public abstract void OnTerminalEndingCall(object sender, CallArgs e);
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
