using System;
using ATE.Args;
using ATE.Entities.ATE;
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
        public EventHandler<CallArgs> AcceptedIncomingCall;
        public EventHandler<CallArgs> AcceptedOutgoingCall;
        public EventHandler<CallArgs> EndedCall;

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
        public abstract void HandleEndedCall(object sender, CallArgs e);
        
        protected virtual void RaiseIncomingCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            Status = PortStatus.InCall;
            IncomingCall?.Invoke(sender, e);
        }
        protected virtual void RaiseOutgoingCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            Status = PortStatus.InCall;
            OutgoingCall?.Invoke(sender, e);
        }
        protected virtual void RaiseAcceptedIncomingCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            AcceptedIncomingCall?.Invoke(sender, e);
        }
        protected virtual void RaiseAcceptedOutgoingCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            AcceptedOutgoingCall?.Invoke(sender, e);
        }
        protected virtual void RaiseEndedCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            Status = PortStatus.Connected;
            EndingCall?.Invoke(sender, e);
        }
        protected virtual void RaiseRejectedCall(object sender, CallArgs e)
        {
            Console.Write($"Port[{CurrentTerminal.Number}]->");
            Status = PortStatus.Connected;
            RejectingCall?.Invoke(sender, e);
        }
    }
}
