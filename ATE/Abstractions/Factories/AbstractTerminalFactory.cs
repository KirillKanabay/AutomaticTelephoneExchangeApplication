using ATE.Entities.Company.Contracts;
using ATE.Entities.Terminal;

namespace ATE.Factories
{
    public abstract class AbstractTerminalFactory
    {
        public abstract BaseTerminal CreateTerminal(BaseContract contract);
    }
}
