namespace ATE.Core.Interfaces.ATE
{
    public interface ITerminalSubscriber
    {
        void SubscribeToTerminal(ITerminal terminal);
        void UnsubscribeFromTerminal(ITerminal terminal);
    }
}