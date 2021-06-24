using System;
using ATE.Core.Args;
using ATE.Core.Entities;

namespace ATE.Core.Interfaces
{
    public interface ITerminal
    {
        string Number { get; }
        Port Port { get; }
        Contract Contract { get; }
        void ConnectTo(AutomaticTelephoneExchange ate);
        void Disconnect();
        void CallTo(string number);
        void HandleIncomingCall(Call call);
        void AcceptIncomingCall(Call call);
        void RejectIncomingCall(Call call);
    }
}