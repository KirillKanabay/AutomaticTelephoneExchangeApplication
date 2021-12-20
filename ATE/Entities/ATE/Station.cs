using System;
using ATE.Args;
using ATE.Core.Interfaces.ATE;
using ATE.Entities.Port;
using ATE.Enums;
using ATE.Interfaces.ATE;

namespace ATE.Entities.ATE
{
    public class Station: IAutomaticTelephoneExchange, ITerminalSubscriber
    {
        private readonly IPortController _portController;
        public Station(IPortController portController)
        {
            _portController = portController;
        }

        public BasePort Connect(ITerminal terminal)
        {
            var port = _portController.GetAvailablePort();
            
            if (port != null)
            {
                port.Connect(terminal);
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
            BasePort targetPort = _portController.GetByPhoneNumber(e.TargetNumber);

            if (targetPort == null)
            {
                throw new Exception("Неправильно набран номер");
            }

            if (targetPort.Status == PortStatus.InCall)
            {
                throw new Exception("Абонент занят. Перезвоните позже.");
            }
            
            targetPort.HandleIncomingCall();
        }

        private void OnTerminalRejectedCall(object sender, CallArgs e)
        {
            IPort targetPort = Port.Port.FindByPhoneNumber(Ports, e.TargetNumber);
            IPort fromPort = Port.Port.FindByPhoneNumber(Ports, e.FromNumber);
            
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
            IPort targetPort = Port.Port.FindByPhoneNumber(Ports, e.TargetNumber);
            IPort fromPort = Port.Port.FindByPhoneNumber(Ports, e.FromNumber);
            
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
