using System;
using ATE.Args;
using ATE.Entities.Terminal;
using ATE.Enums;

namespace ATE.Entities.Port
{
    public abstract class BasePort
    {
        public int Id { get; protected set; }
        public PortStatus Status { get; protected set; }
        public BaseTerminal CurrentTerminal { get; protected set; }
        public abstract void Connect(BaseTerminal terminal);
        public abstract void Disconnect(BaseTerminal terminal);
        public abstract void HandleIncomingCall();
    }
}
