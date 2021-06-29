using ATE.Core.Enums;

namespace ATE.Core.Interfaces.ATE
{
    public interface IPort
    {
        int PortNumber { get; }
        PortStatus Status { get; }
        ITerminal Terminal { get; }
        void ConnectTerminal(ITerminal terminal);
    }
}