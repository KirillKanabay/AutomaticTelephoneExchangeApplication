using System;
using ATE.Args;
using ATE.Entities.Terminal;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public class Port : BasePort
    {
        public EventHandler<CallArgs> OutgoingCall;
        public EventHandler<CallArgs> IncomingCall;
        public EventHandler<CallArgs> EndingCall;
        public EventHandler<CallArgs> RejectingCall;

        public override void Connect(BaseTerminal terminal)
        {
            if (Status == PortStatus.Available)
            {
                CurrentTerminal = terminal;

                CurrentTerminal.CallEvent += OnOutgoingCall;
                CurrentTerminal.CallEndedEvent += OnCallEnding;
                CurrentTerminal.CallRejectedEvent += OnCallRejecting;
            }
            else
            {
                throw new ArgumentException("Port isn't available");
            }
        }

        public override void Disconnect(BaseTerminal terminal)
        {
            if (CurrentTerminal == terminal)
            {
                terminal.CallEvent -= OnOutgoingCall;
                terminal.CallEndedEvent -= OnCallEnding;
                terminal.CallRejectedEvent -= OnCallRejecting;
            }
            else
            {
                throw new ArgumentException("This port doesn't have this terminal");
            }
        }

        public override void HandleIncomingCall()
        {
            if (Status == PortStatus.Connected)
            {
            }
        }

        protected virtual void OnIncomingCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
            IncomingCall?.Invoke(sender, e);
        }

        protected virtual void OnOutgoingCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
            OutgoingCall?.Invoke(sender, e);
        }

        protected virtual void OnCallEnding(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
            EndingCall?.Invoke(sender, e);
        }

        protected virtual void OnCallRejecting(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
            RejectingCall?.Invoke(sender, e);
        }

        // public int PortNumber { get; }
        // public PortStatus Status { get; private set; }
        // public ITerminal Terminal { get; private set; }
        // public override void Connect(ITerminal terminal)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public override void HandleIncomingCall()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Port(int portNumber)
        // {
        //     PortNumber = portNumber;
        // }
        //
        // public void ConnectTerminal(ITerminal terminal)
        // {
        //     if (Terminal != null)
        //     {
        //         throw new Exception("К данному порту уже подключен терминал");
        //     }
        //     
        //     Terminal = terminal;
        //     Status = PortStatus.Connected;
        //     SubscribeToTerminal(Terminal);
        // }
        //
        // public void SubscribeToTerminal(ITerminalObserver terminal)
        // {
        //     
        // }
        //
        // public void UnsubscribeFromTerminal(ITerminalObserver terminal)
        // {
        //     terminal.CallEvent -= OnTerminalCall;
        //     terminal.CallEndedEvent -= OnTerminalCallEnded;
        //     terminal.CallRejectedEvent -= OnTerminalCallRejected;
        //     terminal.DisconnectedEvent -= OnTerminalDisconnected; 
        // }
        //

        //
        // public static IPort FindByPhoneNumber(IEnumerable<IPort> ports, string phoneNumber)
        // {
        //     return ports.FirstOrDefault(t => t.Terminal?.Number == phoneNumber);
        // }

    }
}