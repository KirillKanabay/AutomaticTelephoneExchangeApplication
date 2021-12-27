using ATE.Core.Interfaces.ATE;
using ATE.Entities.Port;

namespace ATE.Interfaces.ATE
{
    public interface IAutomaticTelephoneExchange
    {
        BasePort Connect(ITerminal terminal);
    }
}