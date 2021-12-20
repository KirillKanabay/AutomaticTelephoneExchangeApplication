using ATE.Core.Interfaces.ATE;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public abstract class BasePort
    {
        public int Id { get; protected set; }
        public PortStatus Status { get; protected set; }
        public ITerminal Terminal { get; protected set; }
        public abstract void Connect(ITerminal terminal);
        public abstract void HandleIncomingCall();
    }
}
