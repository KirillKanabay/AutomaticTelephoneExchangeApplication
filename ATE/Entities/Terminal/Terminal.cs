using System;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Port;
using ATE.Enums;
using ATE.Mapper;

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
            var callArgs = new CallArgs()
            {
                Date = DateTime.Now,
                FromNumber = Number,
                TargetNumber = targetNumber
            };

            RaiseStartCallEvent(this, callArgs);
        }
        public override void HandleIncomingCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Await)
            {
                CurrentCall = CallMapper.MapToCall(e);
                RaiseIncomingCallEvent(this, e);
            }
        }
        public override void HandleOutgoingAcceptedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Accepted)
            {
                CurrentCall = CallMapper.MapToCall(e);
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
            if (e.Status == CallStatus.Ended && CurrentCall != null)
            {
                CurrentCall = null;
                RaiseCallEndedEvent(sender, e);
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

                RaiseCallAcceptedEvent(this, CallMapper.MapToArgs(CurrentCall));
            }
        }
        public override void RejectCall()
        {
            if (CurrentCall.Status == CallStatus.Await)
            {
                CurrentCall.Reject();
                var args = CallMapper.MapToArgs(CurrentCall);

                CurrentCall = null;

                RaiseIncomingRejectedCallEvent(this, args);
            }
        }
        public override void EndCall()
        {
            if (CurrentCall is {Status: CallStatus.Accepted})
            {
                var args = CallMapper.MapToArgs(CurrentCall);
                
                CurrentCall = null;
                
                RaiseCallEndedEvent(this, args);
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