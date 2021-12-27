using System;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Port;
using ATE.Enums;

namespace ATE.Entities.Terminal
{
    public class Terminal : BaseTerminal
    {
        public override void ConnectToStation(BaseStation station)
        {
            var stationPort = station.ConnectTerminal(this);
            if (stationPort != null)
            {
                ConnectToPort(stationPort);
            }
        }
        public override void Call(string targetNumber)
        {
            RaiseStartCallEvent(this, new CallArgs() {Call = new Call(Number, targetNumber)});
        }
        public override void HandleIncomingCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Await)
            {
                CurrentCall = e.Call;
                RaiseIncomingCallEvent(this, e);
            }
        }
        public override void HandleOutgoingAcceptedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Accepted)
            {
                CurrentCall = e.Call;
                RaiseOutgoingCallAcceptedEvent(this, e);
            }
        }
        public override void HandleOutgoingRejectedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Rejected)
            {
                CurrentCall = null;
                RaiseOutgoingRejectedCallEvent(sender, e);
            }
        }
        public override void HandleEndCall(object sender, CallArgs e)
        {
            if (CurrentCall is { Status: CallStatus.Ended})
            {
                CurrentCall = null;
                RaiseCallEndedEvent(this, new CallArgs() { Call = CurrentCall });
            }
        }
        public override void AcceptCall()
        {
            if (CurrentCall is {Status: CallStatus.Await})
            {
                CurrentCall.Accept();
                RaiseCallAcceptedEvent(this, new CallArgs(){ Call = CurrentCall });
            }
        }
        public override void RejectCall()
        {
            if (CurrentCall.Status == CallStatus.Await)
            {
                CurrentCall.Reject();
                RaiseIncomingRejectedCallEvent(this, new CallArgs(){Call = CurrentCall});
            }
        }
        public override void EndCall()
        {
            if (CurrentCall is {Status: CallStatus.Accepted})
            {
                CurrentCall.End();
                RaiseCallEndedEvent(this, new CallArgs(){Call = CurrentCall});
                CurrentCall = null;
            }
        }
        private void ConnectToPort(BasePort port)
        {
            CurrentPort = port;

            CurrentPort.IncomingCallEvent += HandleIncomingCall;
            CurrentPort.AcceptedOutgoingCallEvent += HandleOutgoingAcceptedCall;
            CurrentPort.EndedCallEvent += RaiseCallEndedEvent;
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