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
        
        public event EventHandler<CallArgs> OutgoingCallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> IncomingRejectedCallEvent;
        public event EventHandler<CallArgs> OutgoingRejectedCallEvent;
        public event EventHandler<CallArgs> OutgoingCallAcceptedEvent;
        public event EventHandler<CallCanceledArgs> CallCanceledEvent;
        
        public Call CurrentCall { get; set; }
        public BasePort CurrentPort { get; set; }
        public string Number { get; set; }

        public abstract void ConnectToStation(BaseStation station);
        public abstract void Call(string phoneNumber);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        public abstract void HandleOutgoingAcceptedCall(object sender, CallArgs e);
        public abstract void HandleOutgoingRejectedCall(object sender, CallArgs e);
        public abstract void HandleEndCall(object sender, CallArgs e);
        public abstract void HandleCanceledCall(object sender, CallCanceledArgs e);
        public abstract void AcceptCall();
        public abstract void RejectCall();
        public abstract void EndCall();

        protected virtual void RaiseStartCallEvent(object sender, CallArgs e)
        {
            OutgoingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseIncomingCallEvent(object sender, CallArgs e)
        {
            IncomingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseCallAcceptedEvent(object sender, CallArgs e)
        {
            CallAcceptedEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseIncomingRejectedCallEvent(object sender, CallArgs e)
        {
            IncomingRejectedCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseOutgoingRejectedCallEvent(object sender, CallArgs e)
        {
            OutgoingRejectedCallEvent?.Invoke(sender, e);
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
        protected virtual void RaiseCallEndedEvent(object sender, CallArgs e)
        {
            CallEndedEvent?.Invoke(this, e);
        }
        protected virtual void OnCallCanceledEvent(object sender, CallCanceledArgs e)
        {
            CallCanceledEvent?.Invoke(sender, e);
        }
    }
}
