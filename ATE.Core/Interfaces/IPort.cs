using System;
using ATE.Core.Args;
using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Enums;

namespace ATE.Core.Interfaces
{
    public interface IPort
    {
        int PortNumber { get; }
        PortStatus Status { get; }
        ITerminal Terminal { get; }
        void ConnectTerminal(ITerminal terminal);
    }
}