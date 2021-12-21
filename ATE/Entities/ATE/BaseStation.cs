using ATE.Args;
using ATE.Entities.Port;
using ATE.Entities.Terminal;

namespace ATE.Entities.ATE
{
    public abstract class BaseStation
    {
        public abstract BasePort ConnectTerminal(BaseTerminal terminal);
        public abstract void OnTerminalStartingCall(object sender, CallArgs e);
        public abstract void OnTerminalAcceptingCall(object sender, CallArgs e);
        public abstract void OnTerminalRejectingCall(object sender, CallArgs e);
        public abstract void OnTerminalEndingCall(object sender, CallArgs e);
    }
}
