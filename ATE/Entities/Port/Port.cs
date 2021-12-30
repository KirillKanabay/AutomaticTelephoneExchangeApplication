using System;
using ATE.Args;
using ATE.Entities.ATE;
using ATE.Entities.Terminal;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public class Port : BasePort
    {
        public Port(int id) : base(id)
        {
        }

        public override void ConnectToTerminal(BaseTerminal terminal)
        {
            if (Status == PortStatus.Available)
            {
                CurrentTerminal = terminal;

                CurrentTerminal.OutgoingCallEvent += OnOutgoingCall;
                CurrentTerminal.CallEndedEvent += OnEndedCall;
                CurrentTerminal.CallRejectedEvent += RaiseRejectedCall;
                CurrentTerminal.CallAcceptedEvent += OnAcceptedCall;
                CurrentTerminal.CallEndedEvent += OnEndedCall;

                Status = PortStatus.Connected;
            }
            else
            {
                throw new ArgumentException("Port isn't available");
            }
        }
        public override void ConnectToStation(BaseStation station)
        {
            OutgoingCallEvent += station.OnTerminalStartedCall;
            CallAcceptedEvent += station.OnTerminalAcceptingCall;
            IncomingRejectedCallEvent += station.OnTerminalRejectingCall;
            CallEndedEvent += station.OnTerminalEndingCall;

            IsConnectedToStation = true;
        }
        public override void Disconnect(BaseTerminal terminal)
        {
            if (CurrentTerminal == terminal)
            {
                terminal.OutgoingCallEvent -= OnOutgoingCall;
                terminal.CallEndedEvent -= OnEndedCall;
                terminal.CallRejectedEvent -= RaiseRejectedCall;
            }
            else
            {
                throw new ArgumentException("This terminal doesn't have connection with this port");
            }
        }
        public override void HandleIncomingCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.Connected)
            {
                Status = PortStatus.InCall;
                RaiseIncomingCall(sender, e);
            }
        }
        public override void HandleIncomingAcceptedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                OnAcceptedCall(sender, e);
            }
        }
        public override void HandleOutgoingCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                RaiseAcceptedOutgoingCall(sender, e);
            }
        }
        public override void HandleRejectedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                Status = PortStatus.Connected;
                OnRejectedCall(sender, e);
            }
        }
        public override void HandleOutgoingRejectedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                Status = PortStatus.Connected;
                RaiseOutgoingRejectedCall(sender, e);
            }
        }
        public override void HandleEndedCall(object sender, CallArgs e)
        {
            OnEndedCall(sender, e);
        }
        public override void HandleCanceledCall(object sender, CallCanceledArgs e)
        {
            OnCallCanceledEvent(sender, e);
        }
    }
}