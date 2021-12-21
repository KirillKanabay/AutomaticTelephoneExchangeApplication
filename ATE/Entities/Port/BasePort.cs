using System;
using ATE.Args;
using ATE.Entities.Terminal;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public abstract class BasePort
    {
        public EventHandler<CallArgs> OutgoingCall;
        public EventHandler<CallArgs> IncomingCall;
        public EventHandler<CallArgs> EndingCall;
        public EventHandler<CallArgs> RejectingCall;

        public int Id { get; protected set; }
        public PortStatus Status { get; protected set; }
        public BaseTerminal CurrentTerminal { get; protected set; }

        protected BasePort(int id)
        {
            Id = id;
        }

        public abstract void Connect(BaseTerminal terminal);
        public abstract void Disconnect(BaseTerminal terminal);
        public abstract void HandleIncomingCall(object sender, CallArgs e);
        protected virtual void OnIncomingCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
            IncomingCall?.Invoke(sender, e);
        }
        protected virtual void OnOutgoingCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
            OutgoingCall?.Invoke(sender, e);
        }
        protected virtual void OnCallEnding(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
            EndingCall?.Invoke(sender, e);
        }
        protected virtual void OnCallRejecting(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
            RejectingCall?.Invoke(sender, e);
        }
    }
}
