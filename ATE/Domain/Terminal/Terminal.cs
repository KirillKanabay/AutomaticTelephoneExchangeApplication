using System;
using ATE.Abstractions.Domain.Port;
using ATE.Abstractions.Domain.Station;
using ATE.Abstractions.Domain.Terminal;
using ATE.Args;
using ATE.Enums;
using ATE.Mappers;

namespace ATE.Domain.Terminal
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

            IsConnected = true;
            OnConnectedEvent(this, new TerminalArgs());
        }

        public override void Disconnect()
        {
            OnDisconnectedEvent(this, new TerminalArgs());
        }

        public override void Call(string targetNumber)
        {
            var callArgs = new CallArgs
            {
                Date = DateTime.Now,
                SourceNumber = Number,
                TargetNumber = targetNumber,
            };

            OnOutgoingCallEvent(this, callArgs);
        }
        public override void HandleIncomingCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Await)
            {
                CurrentCall = CallMapper.MapToCall(e);
                OnIncomingCallEvent(this, e);
            }
        }
        public override void HandleAcceptedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Accepted)
            {
                CurrentCall = CallMapper.MapToCall(e);
                OnCallAcceptedEvent(this, e);
            }
        }
        public override void HandleRejectedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Rejected)
            {
                OnRejectedCallEvent(sender, e);
                CurrentCall = null;
            }
        }
        public override void HandleEndedCall(object sender, CallArgs e)
        {
            if (e.Status == CallStatus.Ended && CurrentCall != null)
            {
                CurrentCall = null;
                OnCallEndedEvent(sender, e);
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
                OnCallAcceptedEvent(this, CallMapper.MapToArgs(CurrentCall));
                CurrentCall.Accept();
            }
        }
        public override void RejectCall()
        {
            if (CurrentCall.Status == CallStatus.Await)
            {
                OnRejectedCallEvent(this, CallMapper.MapToArgs(CurrentCall));
                CurrentCall = null;
            }
        }
        public override void EndCall()
        {
            if (CurrentCall is {Status: CallStatus.Accepted})
            {
                var args = CallMapper.MapToArgs(CurrentCall);
                
                CurrentCall = null;
                
                OnCallEndedEvent(this, args);
            }
        }
        protected override void ConnectToPort(BasePort port)
        {
            CurrentPort = port;

            CurrentPort.CallAcceptedEvent += HandleAcceptedCall;
            CurrentPort.CallRejectedEvent += HandleRejectedCall;
            CurrentPort.CallCanceledEvent += HandleCanceledCall;
            CurrentPort.IncomingCallEvent += HandleIncomingCall;
            CurrentPort.CallEndedEvent += HandleEndedCall;
        }
    }
}