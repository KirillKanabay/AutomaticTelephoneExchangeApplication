using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities.ATE
{
    public class AutomaticTelephoneExchange  //TODO: сделать интерфейс
    {
        private readonly Company _company;
        
        private ICollection<IPort> Ports { get; set; }
        
        public AutomaticTelephoneExchange(Company company, int portCount)
        {
            _company = company;
            if (portCount <= 0)
            {
                throw new ArgumentException("Количество портов станции не может быть меньше или равно нулю.");
            }
            InitPorts(portCount);
        }

        public IPort Connect(BaseTerminal terminal)
        {
            var port =  Ports.FirstOrDefault(p => p.Terminal == null);
            if (port != null)
            {
                port.ConnectTerminal(terminal);
                
                terminal.CallEvent += OnTerminalCall;
                terminal.DisconnectedEvent += OnTerminalDisconnected;
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }
        
        private void InitPorts(int portCount)
        {
            Ports = new List<IPort>();
            for (var portNumber = 1; portNumber <= portCount; portNumber++)
            {
                var port = new Port(portNumber);
                Ports.Add(port);
            }
        }
        
        private void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            e.Terminal.CallEvent -= OnTerminalCall;
            e.Terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            BaseTerminal targetTerminal = Ports.FirstOrDefault(t => t.Terminal?.Number == e.Call.TargetNumber)?.Terminal;

            if (targetTerminal == null)
            {
                throw new Exception("Неправильно набран номер");
            }

            if (targetTerminal.Port.Status == PortStatus.InCall)
            {
                throw new Exception("Абонент занят. Перезвоните позже.");
            }
            
            targetTerminal.HandleIncomingCall(e.Call);
        }
    }
}
