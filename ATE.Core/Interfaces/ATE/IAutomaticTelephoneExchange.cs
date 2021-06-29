namespace ATE.Core.Interfaces.ATE
{
    public interface IAutomaticTelephoneExchange
    {
        IPort Connect(ITerminal terminal);
    }
}