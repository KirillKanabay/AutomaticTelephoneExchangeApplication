using ATE.Args;
using ATE.Entities.Port;
using ATE.Entities.Terminal;

namespace ATE.Entities.ATE
{
    public abstract class BaseStation
    {
        public abstract BasePort ConnectTerminal(BaseTerminal terminal);
        public abstract void OnTerminalStartingCall(object sender, CallArgs e);
    }
}
