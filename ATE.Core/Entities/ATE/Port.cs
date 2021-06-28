using System;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities.ATE
{
    public class Port : IPort
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
            
            terminal.DisconnectedEvent += OnTerminalDisconnected;
            terminal.CallEvent += OnTerminalCall;
            terminal.CallEndedEvent += OnTerminalCallEnded;
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
        }
        
        public void OnTerminalCallEnded(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
        }

        public void OnTerminalCallRejected(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
        }
        
        public void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            Terminal.CallEvent -= OnTerminalCall;
            Terminal.CallEndedEvent -= OnTerminalCallEnded;
            Terminal.DisconnectedEvent -= OnTerminalDisconnected;
            Terminal.CallRejectedEvent -= OnTerminalCallRejected; 
            
            Terminal = null;
            Status = PortStatus.Disconnected;
        }
    }
}