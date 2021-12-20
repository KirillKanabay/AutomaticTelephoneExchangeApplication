using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;

namespace ATE.Core.Entities.ATE
{
    public class AutomaticTelephoneExchange: IAutomaticTelephoneExchange, ITerminalSubscriber
    {
        public IEnumerable<IPort> Ports { get; private set; }
        
        public AutomaticTelephoneExchange(ICompany company, int portCount)
        {
            if (portCount <= 0)
            {
                throw new ArgumentException("Количество портов станции не может быть меньше или равно нулю.");
            }
            
            Ports = Port.GetPortsCollection(portCount);
        }

        public IPort Connect(ITerminal terminal)
        {
            var port =  Ports.FirstOrDefault(p => p.Terminal == null);
            if (port != null)
            {
                port.ConnectTerminal(terminal);
                SubscribeToTerminal(terminal);
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }
        
        public void SubscribeToTerminal(ITerminalObserver terminal)
        {
            terminal.CallEvent += OnTerminalCall;
            terminal.CallEndedEvent += OnTerminalCallEnded;
            terminal.CallRejectedEvent += OnTerminalRejectedCall;
            terminal.DisconnectedEvent += OnTerminalDisconnected;
        }

        public void UnsubscribeFromTerminal(ITerminalObserver terminal)
        {
            terminal.CallEvent -= OnTerminalCall;
            terminal.CallEndedEvent -= OnTerminalCallEnded;
            terminal.CallRejectedEvent -= OnTerminalRejectedCall;
            terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }
        
        private void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            UnsubscribeFromTerminal(e.Terminal);
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            ITerminal targetTerminal = Port.FindByPhoneNumber(Ports, e.TargetNumber)?.Terminal;

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

        private void OnTerminalRejectedCall(object sender, CallArgs e)
        {
            IPort targetPort = Port.FindByPhoneNumber(Ports, e.TargetNumber);
            IPort fromPort = Port.FindByPhoneNumber(Ports, e.FromNumber);
            
            if (fromPort?.Status == PortStatus.InCall)
            {
                fromPort?.Terminal.RejectCall();
            }
            
            if (targetPort?.Status == PortStatus.InCall)
            {
                targetPort?.Terminal.RejectCall();
            }
        }

        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            IPort targetPort = Port.FindByPhoneNumber(Ports, e.TargetNumber);
            IPort fromPort = Port.FindByPhoneNumber(Ports, e.FromNumber);
            
            if (fromPort?.Status == PortStatus.InCall)
            {
                fromPort?.Terminal.EndCall();
            }
            
            if (targetPort?.Status == PortStatus.InCall)
            {
                targetPort?.Terminal.EndCall();
            }
        }
        
    }
}
