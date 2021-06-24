using System;
using ATE.Core.Args;
using ATE.Core.Entities;
using ATE.Core.Enums;

namespace ATE.Core.Interfaces
{
    public interface IPort
    {
        int PortNumber { get; }
        PortStatus Status { get; }
        BaseTerminal Terminal { get; }
        void ConnectTerminal(BaseTerminal terminal);
        void OnTerminalDisconnected(object sender, TerminalArgs e);
        void OnTerminalCall(object sender, CallArgs e);
    }
}