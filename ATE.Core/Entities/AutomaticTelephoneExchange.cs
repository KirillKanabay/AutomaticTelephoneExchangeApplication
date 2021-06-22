using System.Collections.Generic;
using System.Linq;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class AutomaticTelephoneExchange
    {
        private readonly Company _company;
        public IDictionary<Port, ITerminal> Ports { get; private set; }

        public AutomaticTelephoneExchange(Company company, int portCount)
        {
            _company = company;
            InitPorts(portCount);
        }

        private void InitPorts(int portCount)
        {
            Ports = new Dictionary<Port, ITerminal>();
            for (var portNumber = 1; portNumber <= portCount; portNumber++)
            {
                var port = new Port(portNumber, PortStatus.Disconnected);
                Ports.Add(port, null);
            }
        }

        public Port Connect(ITerminal terminal)
        {
            return Ports.FirstOrDefault(p => p.Value == null).Key;
        }
    }
}
