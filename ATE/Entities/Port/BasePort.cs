using System;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Terminal;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public abstract class BasePort
    {
        public EventHandler<CallArgs> OutgoingCallEvent;
        public EventHandler<CallArgs> IncomingCallEvent;
        public EventHandler<CallArgs> EndingCallEvent;
        public EventHandler<CallArgs> IncomingRejectedCallEvent;
        public EventHandler<CallArgs> OutgoingRejectedCallEvent;
        public EventHandler<CallArgs> AcceptedIncomingCallEvent;
        public EventHandler<CallArgs> AcceptedOutgoingCallEvent;
        public EventHandler<CallArgs> EndedCallEvent;

        public int Id { get; protected set; }
        public PortStatus Status { get; protected set; }
        public bool IsConnectedToStation { get; protected set; }
        public BaseTerminal CurrentTerminal { get; protected set; }

        protected BasePort(int id)
        {
            Id = id;
        }

        public abstract void ConnectToTerminal(BaseTerminal terminal);
        public abstract void ConnectToStation(BaseStation station);
        public abstract void Disconnect(BaseTerminal terminal);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        public abstract void HandleIncomingAcceptedCall(object sender, CallArgs e);
        public abstract void HandleOutgoingAcceptedCall(object sender, CallArgs e);
        public abstract void HandleIncomingRejectedCall(object sender, CallArgs e);
        public abstract void HandleOutgoingRejectedCall(object sender, CallArgs e);
        public abstract void HandleEndedCall(object sender, CallArgs e);
        
        protected virtual void RaiseIncomingCall(object sender, CallArgs e)
        {
            IncomingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseOutgoingCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
            OutgoingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseAcceptedIncomingCall(object sender, CallArgs e)
        {
            AcceptedIncomingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseAcceptedOutgoingCall(object sender, CallArgs e)
        {
            AcceptedOutgoingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseIncomingRejectedCall(object sender, CallArgs e)
        {
            IncomingRejectedCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseOutgoingRejectedCall(object sender, CallArgs e)
        {
            OutgoingRejectedCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseEndedCall(object sender, CallArgs e)
        {
            EndingCallEvent?.Invoke(sender, e);
        }
        protected virtual void RaiseRejectedCall(object sender, CallArgs e)
        {
            IncomingRejectedCallEvent?.Invoke(sender, e);
        }
    }
}
