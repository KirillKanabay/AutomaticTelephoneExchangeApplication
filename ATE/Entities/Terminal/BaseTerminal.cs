using System;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Port;

namespace ATE.Entities.Terminal
{
    public abstract class BaseTerminal
    {
        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        
        public event EventHandler<CallArgs> StartCallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        public event EventHandler<CallArgs> OutgoingCallAcceptedEvent;
        
        public Call CurrentCall { get; set; }
        public BasePort CurrentPort { get; set; }
        public string Number { get; set; }

        public abstract void ConnectToStation(BaseStation station);
        public abstract void Call(string phoneNumber);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        public abstract void HandleOutgoingAcceptedCall(object sender, CallArgs e);
        public abstract void AcceptCall();
        public abstract void RejectCall();

        protected virtual void RaiseStartCallEvent(object sender, CallArgs e)
        {
            StartCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseIncomingCallEvent(object sender, CallArgs e)
        {
            IncomingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseCallAcceptedEvent(object sender, CallArgs e)
        {
            CallAcceptedEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseCallRejectedEvent(object sender, CallArgs e)
        {
            CallRejectedEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseOutgoingCallAcceptedEvent(object sender, CallArgs e)
        {
            OutgoingCallAcceptedEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseTerminalDisconnectedEvent()
        {
            var args = new TerminalArgs(this);
            DisconnectedEvent?.Invoke(this, args);
        }
        protected virtual void RaiseCallEndedEvent(Call call)
        {
            CallEndedEvent?.Invoke(this, new CallArgs());
        }
    }
}
