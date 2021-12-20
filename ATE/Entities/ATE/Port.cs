using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Args;
using ATE.Core.Interfaces.ATE;
using ATE.Enums;

namespace ATE.Entities.ATE
{
    public class Port : IPort, ITerminalSubscriber
    {
        public int PortNumber { get; }
        public PortStatus Status { get; private set; }
        public ITerminal Terminal { get; private set; }
        
        public Port(int portNumber)
        {
            PortNumber = portNumber;
        }

        public void ConnectTerminal(ITerminal terminal)
        {
            if (Terminal != null)
            {
                throw new Exception("К данному порту уже подключен терминал");
            }
            
            Terminal = terminal;
            Status = PortStatus.Connected;
            SubscribeToTerminal(Terminal);
        }
        
        public void SubscribeToTerminal(ITerminalObserver terminal)
        {
            terminal.CallEvent += OnTerminalCall;
            terminal.CallEndedEvent += OnTerminalCallEnded;
            terminal.CallRejectedEvent += OnTerminalCallRejected;
            terminal.DisconnectedEvent += OnTerminalDisconnected;
        }

        public void UnsubscribeFromTerminal(ITerminalObserver terminal)
        {
            terminal.CallEvent -= OnTerminalCall;
            terminal.CallEndedEvent -= OnTerminalCallEnded;
            terminal.CallRejectedEvent -= OnTerminalCallRejected;
            terminal.DisconnectedEvent -= OnTerminalDisconnected; 
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
        }
        
        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
        }

        private void OnTerminalCallRejected(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
        }
        
        private void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            UnsubscribeFromTerminal(Terminal);
            Terminal = null;
            Status = PortStatus.Disconnected;
        }

        public static IPort FindByPhoneNumber(IEnumerable<IPort> ports, string phoneNumber)
        {
            return ports.FirstOrDefault(t => t.Terminal?.Number == phoneNumber);
        }

        public static IEnumerable<IPort> GetPortsCollection(int portsCount)
        {
            var ports = new List<IPort>();
            for (var portNumber = 1; portNumber <= portsCount; portNumber++)
            {
                var port = new Port(portNumber);
                ports.Add(port);
            }

            return ports;
        }
    }
}