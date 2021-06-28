using System;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;

namespace ATE.Core.Interfaces
{
    public interface ITerminal
    {
        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        public event EventHandler<CallArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        
        string Number { get; }
        IPort Port { get; }
        IContract Contract { get; }
        Call CurrentCall { get; }
        
        void ConnectTo(AutomaticTelephoneExchange ate);
        void Disconnect();
        void CallTo(string targetNumber);
        void ResetCall();
        void HandleIncomingCall(Call call);
        void AcceptIncomingCall();
        void RejectIncomingCall();
        void EndCall();
    }
}