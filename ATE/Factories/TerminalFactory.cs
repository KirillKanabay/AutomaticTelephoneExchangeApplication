using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Domain.Terminal;
using ATE.Domain.Company.Contracts;
using ATE.Domain.Terminal;

namespace ATE.Factories
{
    public class TerminalFactory : AbstractTerminalFactory
    {
        public override BaseTerminal CreateTerminal(BaseContract contract)
        {
            return new Terminal()
            {
                Number = contract.PhoneNumber
            };
        }
    }
}
