using System;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Port : IPort
    {
        public int PortNumber { get; }
        public PortStatus Status { get; private set; }
        public BaseTerminal Terminal { get; private set; }
        public Port(int portNumber)
        {
            PortNumber = portNumber;
        }

        public void ConnectTerminal(BaseTerminal terminal)
        {
            if (Terminal != null)
            {
                throw new Exception("К данному порту уже подключен терминал");
            }
            
            Terminal = terminal;
            Status = PortStatus.Connected;
            
            terminal.DisconnectedEvent += OnTerminalDisconnected;
            terminal.CallEvent += OnTerminalCall;
        }
        public void OnTerminalCall(object sender, CallArgs e)
        {
            Status = PortStatus.InCall;
        }
        public void OnTerminalEndCall(object sender, CallArgs e)
        {
            Status = PortStatus.Connected;
        }
        public void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            Terminal.CallEvent -= OnTerminalCall;
            Terminal.CallEvent -= OnTerminalEndCall;
            Terminal.DisconnectedEvent -= OnTerminalDisconnected;
            Terminal = null;
            Status = PortStatus.Disconnected;
        }
    }
}