using ATE.Core.Entities;
using ATE.Core.Entities.ATE;

namespace ATE.Core.Interfaces
{
    public interface ITerminal
    {
        string Number { get; }
        IPort Port { get; }
        IContract Contract { get; }
        
        void ConnectTo(AutomaticTelephoneExchange ate);
        void Disconnect();
        void CallTo(string targetNumber);
        void EndCall(Call call);
        void HandleIncomingCall(Call call);
        void AcceptIncomingCall(Call call);
        void RejectIncomingCall(Call call);
    }
}