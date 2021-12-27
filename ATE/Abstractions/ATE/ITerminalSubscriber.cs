using ATE.Entities.Terminal;

namespace ATE.Interfaces.ATE
{
    public interface ITerminalSubscriber
    {
        void SubscribeToTerminal(BaseTerminal terminal);
        void UnsubscribeFromTerminal(BaseTerminal terminal);
    }
}