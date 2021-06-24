using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class AutomaticTelephoneExchange
    {
        private readonly Company _company;
        public IDictionary<Port, BaseTerminal> Ports { get; private set; }

        public AutomaticTelephoneExchange(Company company, int portCount)
        {
            _company = company;
            InitPorts(portCount);
        }

        private void InitPorts(int portCount)
        {
            Ports = new Dictionary<Port, BaseTerminal>();
            for (var portNumber = 1; portNumber <= portCount; portNumber++)
            {
                var port = new Port(portNumber, PortStatus.Disconnected);
                Ports.Add(port, null);
            }
        }

        public Port Connect(BaseTerminal terminal)
        {
            var port =  Ports.FirstOrDefault(p => p.Value == null).Key;
            if (port != null)
            {
                Ports[port] = terminal;
                port.Status = PortStatus.Connected;

                terminal.CallEvent += OnTerminalCall;
                terminal.CallEvent += port.OnTerminalCall;
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }

        public void Disconnect(ITerminal terminal)
        {
            var port = Ports.FirstOrDefault(p => p.Value == terminal);
            port.Key.Status = PortStatus.Disconnected;
        }

        public void OnTerminalCall(object sender, TerminalArgs e)
        {
            ITerminal destinationTerminal = Ports.Values.FirstOrDefault(t => t.Number == e.TargetNumber);

            if (destinationTerminal == null)
            {
                throw new Exception("Неправильно набран номер");
            }

            if (destinationTerminal.Port.Status == PortStatus.InCall)
            {
                throw new Exception("Абонент занят. Перезвоните позже.");
            }

            Call call = new Call(e.FromTerminal.Number, e.TargetNumber);
            
            destinationTerminal.HandleIncomingCall(call);
        }
    }
}
