namespace ATE.Core.Interfaces.ATE
{
    public interface ITerminalSubscriber
    {
        void SubscribeToTerminal(ITerminalObserver terminal);
        void UnsubscribeFromTerminal(ITerminalObserver terminal);
    }
}