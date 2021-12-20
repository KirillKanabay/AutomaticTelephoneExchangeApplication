using ATE.Entities.ATE;

namespace ATE.Entities.Terminal
{
    public class Terminal : BaseTerminal
    {
        public override void ConnectToStation(BaseStation station)
        {
            station.ConnectTerminal(this);
        }

        // #region Props
        //
        // public IPort Port { get; protected set; }
        // public IContract Contract { get; }
        // public string Number => Contract.PhoneNumber;
        // public Call CurrentCall { get; private set; }
        //
        // #endregion
        //
        // #region Ctors
        //
        // public Terminal(IContract contract)
        // {
        //     Contract = contract ?? throw new ArgumentNullException(nameof(contract), "Договор не может быть null");
        // }
        //
        // #endregion
        //
        // #region Methods
        //
        // public void ConnectTo(IAutomaticTelephoneExchange ate)
        // {
        //     if (Port != null)
        //     {
        //         throw new Exception("Терминал уже подключен к АТС");
        //     }
        //
        //     Port = ate.Connect(this);
        //     
        //     RaiseTerminalConnectedEvent();
        // }
        //
        // public void Disconnect()
        // {
        //     if (Port == null)
        //     {
        //         throw new Exception("Терминал не подключен к АТС");
        //     }
        //     
        //     RaiseTerminalDisconnectedEvent();
        //
        //     Port = null;
        //     
        // }
        //
        // public void CallTo(string targetNumber)
        // {
        //     if (Port.Status == PortStatus.InCall)
        //     {
        //         throw new Exception("Терминал уже находится в состоянии вызова");
        //     }
        //
        //     CurrentCall = new Call(Number, targetNumber);
        //     RaiseCallEvent(CurrentCall);
        // }
        //
        // public void ResetCall()
        // {
        //     CurrentCall = null;
        // }
        //
        // public void HandleIncomingCall(Call call)
        // {
        //     CurrentCall = call;
        //     RaiseIncomingCallEvent(CurrentCall);
        // }
        //
        // public void AcceptIncomingCall()
        // {
        //     CurrentCall.Accept();
        //     RaiseCallAcceptedEvent(CurrentCall);
        // }
        //
        // public void RejectCall()
        // {
        //     CurrentCall.Reject();
        //     RaiseCallRejectedEvent(CurrentCall);
        //     ResetCall();
        // }
        //
        // public void EndCall()
        // {
        //     CurrentCall.End();
        //     RaiseCallEndedEvent(CurrentCall);
        //     ResetCall();
        // }
        //
        // #endregion
        //
        // #region RaiseEvents
        //
        //
        //
        // #endregion
        //
        // public override void ConnectToStation(Station ate)
        // {
        //     throw new NotImplementedException();
        // }
    }
}