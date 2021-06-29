using System.Collections.Generic;

namespace ATE.Core.Interfaces.ATE
{
    public interface IAutomaticTelephoneExchange
    {
        ICollection<IPort> Ports { get; }
        IPort Connect(ITerminal terminal);
    }
}