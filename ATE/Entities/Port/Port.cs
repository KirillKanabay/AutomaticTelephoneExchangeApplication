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

                CurrentTerminal.OutgoingCallEvent += RaiseOutgoingCall;
                CurrentTerminal.CallEndedEvent += RaiseEndedCall;
                CurrentTerminal.IncomingRejectedCallEvent += RaiseRejectedCall;
                CurrentTerminal.CallAcceptedEvent += RaiseAcceptedIncomingCall;
                CurrentTerminal.CallEndedEvent += RaiseEndedCall;

                Status = PortStatus.Connected;
            }
            else
            {
                throw new ArgumentException("Port isn't available");
            }
        }
        public override void ConnectToStation(BaseStation station)
        {
            OutgoingCallEvent += station.OnTerminalStartingCall;
            AcceptedIncomingCallEvent += station.OnTerminalAcceptingCall;
            IncomingRejectedCallEvent += station.OnTerminalRejectingCall;
            EndedCallEvent += station.OnTerminalRejectingCall;

            IsConnectedToStation = true;
        }
        public override void Disconnect(BaseTerminal terminal)
        {
            if (CurrentTerminal == terminal)
            {
                terminal.OutgoingCallEvent -= RaiseOutgoingCall;
                terminal.CallEndedEvent -= RaiseEndedCall;
                terminal.IncomingRejectedCallEvent -= RaiseRejectedCall;
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
                RaiseAcceptedIncomingCall(sender, e);
            }
        }
        public override void HandleOutgoingAcceptedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                RaiseAcceptedOutgoingCall(sender, e);
            }
        }
        public override void HandleIncomingRejectedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                Status = PortStatus.Connected;
                RaiseIncomingRejectedCall(sender, e);
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
            if (Status == PortStatus.InCall)
            {
                Status = PortStatus.Connected;
                RaiseEndedCall(this, e);
            }
        }
    }
}