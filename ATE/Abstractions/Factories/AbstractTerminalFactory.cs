using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Domain.Terminal;
using ATE.Domain.Company.Contracts;
using ATE.Domain.Terminal;

namespace ATE.Factories
{
    public abstract class AbstractTerminalFactory
    {
        public abstract BaseTerminal CreateTerminal(BaseContract contract);
    }
}
