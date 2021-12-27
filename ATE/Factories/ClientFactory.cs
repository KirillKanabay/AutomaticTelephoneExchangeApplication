using ATE.Constants;
using ATE.Entities.Company;
using ATE.Entities.Users;

namespace ATE.Factories
{
    public class ClientFactory : AbstractClientFactory
    {
        private readonly AbstractContractFactory _contractFactory;
        private readonly AbstractTerminalFactory _terminalFactory;

        public ClientFactory(AbstractContractFactory contractFactory, AbstractTerminalFactory terminalFactory)
        {
            _contractFactory = contractFactory;
            _terminalFactory = terminalFactory;
        }

        public override Client CreateClient(User user, BaseCompany company)
        {
            var contract = _contractFactory.CreateContract(company);
            var terminal = _terminalFactory.CreateTerminal(contract);
            
            return new Client()
            {
                User = user,
                Balance = DataConstants.DefaultClientBalance,
                Contract = contract,
                Terminal = terminal,
            };
        }
    }
}