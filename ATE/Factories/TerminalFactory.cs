using ATE.Entities.Company.Contracts;
using ATE.Entities.Terminal;

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
