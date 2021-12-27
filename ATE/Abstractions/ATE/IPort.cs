using ATE.Core.Interfaces.ATE;
using ATE.Enums;

namespace ATE.Interfaces.ATE
{
    public interface IPort
    {
        int PortNumber { get; }
        PortStatus Status { get; }
        ITerminal Terminal { get; }
        void ConnectTerminal(ITerminal terminal);
    }
}