using System;
using ATE.Args;
using ATE.Entities.ATE;

namespace ATE.Entities.Terminal
{
    public abstract class BaseTerminal
    {
        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        public event EventHandler<CallArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;

        public string PhoneNumber { get; set; }

        public abstract void ConnectToStation(BaseStation station);

        protected virtual void RaiseTerminalConnectedEvent()
        {
            var args = new TerminalArgs(this);
            ConnectedEvent?.Invoke(this, args);
        }

        protected virtual void RaiseTerminalDisconnectedEvent()
        {
            var args = new TerminalArgs(this);
            DisconnectedEvent?.Invoke(this, args);
        }

        protected virtual void RaiseCallEvent(Call call)
        {
            CallEvent?.Invoke(this, new CallArgs(call));
        }

        protected virtual void RaiseIncomingCallEvent(Call call)
        {
            IncomingCallEvent?.Invoke(this, new CallArgs(call));
        }

        protected virtual void RaiseCallEndedEvent(Call call)
        {
            CallEndedEvent?.Invoke(this, new CallArgs(call));
        }

        protected virtual void RaiseCallAcceptedEvent(Call call)
        {
            CallAcceptedEvent?.Invoke(this, new CallArgs(call));
        }

        protected virtual void RaiseCallRejectedEvent(Call call)
        {
            CallRejectedEvent?.Invoke(this, new CallArgs(call));
        }

    }
}
