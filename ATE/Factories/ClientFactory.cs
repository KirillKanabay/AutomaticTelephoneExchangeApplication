using System;
using ATE.Abstractions.Domain.Company;
using ATE.Constants;
using ATE.Domain.Company;
using ATE.Domain.Users;

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
                Id = Guid.NewGuid(),
                User = user,
                Balance = DataConstants.DefaultClientBalance,
                Contract = contract,
                Terminal = terminal,
            };
        }
    }
}