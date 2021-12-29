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
            RaiseStartCallEvent(this, new CallArgs(new Call(Number, targetNumber)));
        }
        public override void HandleIncomingCall(object sender, CallArgs e)
        {
            if (e?.Call?.Status == CallStatus.Await)
            {
                CurrentCall = e.Call;
                RaiseIncomingCallEvent(this, e);
            }
        }
        public override void HandleOutgoingAcceptedCall(object sender, CallArgs e)
        {
            if (e?.Call?.Status == CallStatus.Accepted)
            {
                CurrentCall = e.Call;
                RaiseOutgoingCallAcceptedEvent(this, e);
            }
        }
        public override void HandleOutgoingRejectedCall(object sender, CallArgs e)
        {
            if (e?.Call?.Status == CallStatus.Rejected)
            {
                CurrentCall = null;
                RaiseOutgoingRejectedCallEvent(sender, e);
            }
        }
        public override void HandleEndCall(object sender, CallArgs e)
        {
            if (CurrentCall is { Status: CallStatus.Ended})
            {
                RaiseCallEndedEvent(this, new CallArgs(CurrentCall));
                CurrentCall = null;
            }
        }
        public override void HandleCanceledCall(object sender, CallCanceledArgs e)
        {
            CurrentCall = null;
            OnCallCanceledEvent(sender, e);
        }
        public override void AcceptCall()
        {
            if (CurrentCall is {Status: CallStatus.Await})
            {
                CurrentCall.Accept();
                RaiseCallAcceptedEvent(this, new CallArgs(CurrentCall));
            }
        }
        public override void RejectCall()
        {
            if (CurrentCall.Status == CallStatus.Await)
            {
                CurrentCall.Reject();
                RaiseIncomingRejectedCallEvent(this, new CallArgs(CurrentCall));
            }
        }
        public override void EndCall()
        {
            if (CurrentCall is {Status: CallStatus.Accepted})
            {
                var tempCall = CurrentCall;
                
                CurrentCall = null;
                
                RaiseCallEndedEvent(this, new CallArgs(tempCall));
            }
        }

        private void ConnectToPort(BasePort port)
        {
            CurrentPort = port;

            CurrentPort.IncomingCallEvent += HandleIncomingCall;
            CurrentPort.AcceptedOutgoingCallEvent += HandleOutgoingAcceptedCall;
            CurrentPort.EndedCallEvent += HandleEndCall;
            CurrentPort.CallCanceledEvent += HandleCanceledCall;
        }
    }
}