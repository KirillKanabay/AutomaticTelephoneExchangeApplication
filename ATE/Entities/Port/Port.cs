﻿using System;
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

                CurrentTerminal.OutgoingCallEvent += HandleOutgoingCall;
                CurrentTerminal.CallEndedEvent += HandleEndedCall;
                CurrentTerminal.CallRejectedEvent += HandleRejectedCall;
                CurrentTerminal.CallAcceptedEvent += HandleAcceptedCall;

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
            CallAcceptedEvent += station.OnTerminalAcceptedCall;
            IncomingCallEvent += station.OnTerminalRejectedCall;
            CallEndedEvent += station.OnTerminalEndedCall;

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
                Status = PortStatus.AwaitConfirmCall;
                OnIncomingCallEvent(sender, e);
            }
        }
        public override void HandleOutgoingCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.Connected)
            {
                Status = PortStatus.AwaitConfirmCall;
                OnOutgoingCallEvent(sender, e);
            }
        }
        public override void HandleRejectedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.AwaitConfirmCall)
            {
                Status = PortStatus.Connected;
                OnRejectedCallEvent(sender, e);
            }
        }
        public override void HandleAcceptedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.AwaitConfirmCall)
            {
                Status |= PortStatus.InCall;
                OnAcceptedCallEvent(sender, e);
            }
        }
        public override void HandleEndedCall(object sender, CallArgs e)
        {
            if (Status == PortStatus.InCall)
            {
                Status = PortStatus.Connected;
                OnEndedCallEvent(sender, e);
            }
        }
        public override void HandleCanceledCall(object sender, CallCanceledArgs e)
        {
            Status = PortStatus.Connected;
            OnCallCanceledEvent(sender, e);
        }
    }
}