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

                CurrentTerminal.StartCallEvent += RaiseOutgoingCall;
                CurrentTerminal.CallEndedEvent += RaiseEndedCall;
                CurrentTerminal.CallRejectedEvent += RaiseRejectedCall;
                CurrentTerminal.CallAcceptedEvent += RaiseAcceptedIncomingCall;

                Status = PortStatus.Connected;
            }
            else
            {
                throw new ArgumentException("Port isn't available");
            }
        }

        public override void ConnectToStation(BaseStation station)
        {
            OutgoingCall += station.OnTerminalStartingCall;
            AcceptedIncomingCall += station.OnTerminalAcceptingCall;

            IsConnectedToStation = true;
        }

        public override void Disconnect(BaseTerminal terminal)
        {
            if (CurrentTerminal == terminal)
            {
                terminal.StartCallEvent -= RaiseOutgoingCall;
                terminal.CallEndedEvent -= RaiseEndedCall;
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

        public override void HandleEndedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                RaiseEndedCall(this, e);
            }
        }
    }
}