using System;
using ATE.Abstractions.Domain.Station;
using ATE.Abstractions.Domain.Terminal;
using ATE.Args;
using ATE.Enums;

namespace ATE.Abstractions.Domain.Port
{
    public abstract class BasePort
    {
        public event EventHandler<CallArgs> OutgoingCallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallCanceledArgs> CallCanceledEvent;

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
        public abstract void HandleDisconnectedTerminal(object sender, TerminalArgs e);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        public abstract void HandleOutgoingCall(object sender, CallArgs e);
        public abstract void HandleRejectedCall(object sender, CallArgs e);
        public abstract void HandleAcceptedCall(object sender, CallArgs e);
        public abstract void HandleEndedCall(object sender, CallArgs e);
        public abstract void HandleCanceledCall(object sender, CallCanceledArgs e);

        protected virtual void OnIncomingCallEvent(object sender, CallArgs e)
        {
            IncomingCallEvent?.Invoke(sender, e);
        }

        protected virtual void OnOutgoingCallEvent(object sender, CallArgs e)
        {
            OutgoingCallEvent?.Invoke(sender, e);
        }
        
        protected virtual void OnAcceptedCallEvent(object sender, CallArgs e)
        {
            CallAcceptedEvent?.Invoke(sender, e);
        }
        
        protected virtual void OnRejectedCallEvent(object sender, CallArgs e)
        {
            CallRejectedEvent?.Invoke(sender, e);
        }
        
        protected virtual void OnEndedCallEvent(object sender, CallArgs e)
        {
            CallEndedEvent?.Invoke(sender, e);
        }
        
        protected virtual void OnCallCanceledEvent(object sender, CallCanceledArgs e)
        {
            CallCanceledEvent?.Invoke(sender, e);
        }
    }
}