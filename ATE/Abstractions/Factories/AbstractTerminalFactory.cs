using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Domain.Terminal;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractTerminalFactory
    {
        public abstract BaseTerminal CreateTerminal(BaseContract contract);
    }
}
