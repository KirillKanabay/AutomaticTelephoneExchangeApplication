using ATE.Core.Entities.ATE;

namespace ATE.Core.Interfaces.ATE
{
    public interface IAutomaticTelephoneExchange
    {
        IPort Connect(BaseTerminal terminal);
    }
}