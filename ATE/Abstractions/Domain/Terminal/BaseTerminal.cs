using System;
using ATE.Abstractions.Domain.Port;
using ATE.Abstractions.Domain.Station;
using ATE.Args;
using ATE.Domain.Calls;

namespace ATE.Abstractions.Domain.Terminal
{
    public abstract class BaseTerminal
    {
        public event EventHandler<TerminalArgs> ConnectedEvent; 
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        public event EventHandler<CallArgs> OutgoingCallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallCanceledArgs> CallCanceledEvent;
        
        public Call CurrentCall { get; set; }
        public BasePort CurrentPort { get; set; }
        public string Number { get; set; }
        public bool IsConnected { get; protected set; }

        public abstract void ConnectToStation(BaseStation station);
        public abstract void Disconnect();
        public abstract void Call(string phoneNumber);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        public abstract void HandleAcceptedCall(object sender, CallArgs e);
        public abstract void HandleRejectedCall(object sender, CallArgs e);
        public abstract void HandleEndedCall(object sender, CallArgs e);
        public abstract void HandleCanceledCall(object sender, CallCanceledArgs e);
        public abstract void AcceptCall();
        public abstract void RejectCall();
        public abstract void EndCall();
        protected abstract void ConnectToPort(BasePort port);
        protected abstract void DisconnectFromPort();

        protected virtual void OnOutgoingCallEvent(object sender, CallArgs e)
        {
            OutgoingCallEvent?.Invoke(sender, e);
        }

        protected virtual void OnIncomingCallEvent(object sender, CallArgs e)
        {
            IncomingCallEvent?.Invoke(sender, e);
        }

        protected virtual void OnCallAcceptedEvent(object sender, CallArgs e)
        {
            CallAcceptedEvent?.Invoke(sender, e);
        }

        protected virtual void OnRejectedCallEvent(object sender, CallArgs e)
        {
            CallRejectedEvent?.Invoke(sender, e);
        }

        protected virtual void OnCallEndedEvent(object sender, CallArgs e)
        {
            CallEndedEvent?.Invoke(this, e);
        }

        protected virtual void OnCallCanceledEvent(object sender, CallCanceledArgs e)
        {
            CallCanceledEvent?.Invoke(sender, e);
        }

        protected virtual void OnConnectedEvent(object sender, TerminalArgs e)
        {
            ConnectedEvent?.Invoke(sender, e);
        }

        protected virtual void OnDisconnectedEvent(object sender, TerminalArgs e)
        {
            DisconnectedEvent?.Invoke(this, e);
        }
    }
}
