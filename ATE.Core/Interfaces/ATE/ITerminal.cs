using ATE.Core.Entities.ATE;

namespace ATE.Core.Interfaces.ATE
{
    public interface ITerminal : ITerminalObserver
    {
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