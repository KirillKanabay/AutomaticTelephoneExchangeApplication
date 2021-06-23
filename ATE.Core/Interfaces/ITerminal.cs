using System;
using ATE.Core.Args;
using ATE.Core.Entities;

namespace ATE.Core.Interfaces
{
    public interface ITerminal
    {
        event EventHandler<TerminalArgs> CallEvent;
        event EventHandler<CallArgs> IncomingCallEvent;
        event EventHandler<CallArgs> CallAcceptedEvent;
        event EventHandler<CallArgs> CallEndedEvent;
        event EventHandler<CallArgs> CallRejectedEvent; 
        string Number { get; }
        Port Port { get; }
        void ConnectToPort(AutomaticTelephoneExchange ate);
        void CallTo(string number);
        void HandleIncomingCall(Call call);
        void AcceptIncomingCall(Call call);
        void RejectIncomingCall(Call call);
    }
}