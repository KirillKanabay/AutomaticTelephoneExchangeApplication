using System;
using ATE.Args;
using ATE.Entities.Port;
using ATE.Entities.Terminal;

namespace ATE.Entities.ATE
{
    public class Station: BaseStation
    {
        private readonly IPortController _portController;
        public Station(IPortController portController)
        {
            _portController = portController;
        }

        public override BasePort ConnectTerminal(BaseTerminal terminal)
        {
            var port = _portController.GetAvailablePort();

            if (port != null)
            {
                port.Connect(terminal);

                //todo: в controller
                port.OutgoingCall += OnTerminalStartingCall;
                port.AcceptedIncomingCall += OnTerminalAcceptingCall;
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }

        public override void OnTerminalStartingCall(object sender, CallArgs e)
        {
            Console.Write("Station->");
            var port = _portController.GetByPhoneNumber(e.TargetNumber);
            if (port == null)
            {
                throw new ArgumentException("Неправильно набран номер");
            }

            port.HandleIncomingCall(sender, e);
        }

        public override void OnTerminalAcceptingCall(object sender, CallArgs e)
        {
            Console.Write("Station->");
            var port = _portController.GetByPhoneNumber(e.FromNumber);
            port.HandleOutgoingAcceptedCall(this, e);
        }

        public override void OnTerminalRejectingCall(object sender, CallArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnTerminalEndingCall(object sender, CallArgs e)
        {
            throw new NotImplementedException();
        }


        //
        // public void SubscribeToTerminal(ITerminalObserver terminal)
        // {
        //     terminal.CallEvent += OnTerminalCall;
        //     terminal.CallEndedEvent += OnTerminalCallEnded;
        //     terminal.CallRejectedEvent += OnTerminalRejectedCall;
        //     terminal.DisconnectedEvent += OnTerminalDisconnected;
        // }
        //
        // public void UnsubscribeFromTerminal(ITerminalObserver terminal)
        // {
        //     terminal.CallEvent -= OnTerminalCall;
        //     terminal.CallEndedEvent -= OnTerminalCallEnded;
        //     terminal.CallRejectedEvent -= OnTerminalRejectedCall;
        //     terminal.DisconnectedEvent -= OnTerminalDisconnected;
        // }
        //
        // private void OnTerminalDisconnected(object sender, TerminalArgs e)
        // {
        //     UnsubscribeFromTerminal(e.Terminal);
        // }
        //
        // private void OnTerminalCall(object sender, CallArgs e)
        // {
        //     BasePort targetPort = _portController.GetByPhoneNumber(e.TargetNumber);
        //
        //     if (targetPort == null)
        //     {
        //         throw new Exception("Неправильно набран номер");
        //     }
        //
        //     if (targetPort.Status == PortStatus.InCall)
        //     {
        //         throw new Exception("Абонент занят. Перезвоните позже.");
        //     }
        //     
        //     targetPort.HandleIncomingCall();
        // }
        //
        // private void OnTerminalRejectedCall(object sender, CallArgs e)
        // {
        //     IPort targetPort = Port.Port.FindByPhoneNumber(Ports, e.TargetNumber);
        //     IPort fromPort = Port.Port.FindByPhoneNumber(Ports, e.FromNumber);
        //     
        //     if (fromPort?.Status == PortStatus.InCall)
        //     {
        //         fromPort?.Terminal.RejectCall();
        //     }
        //     
        //     if (targetPort?.Status == PortStatus.InCall)
        //     {
        //         targetPort?.Terminal.RejectCall();
        //     }
        // }
        //
        // private void OnTerminalCallEnded(object sender, CallArgs e)
        // {
        //     IPort targetPort = Port.Port.FindByPhoneNumber(Ports, e.TargetNumber);
        //     IPort fromPort = Port.Port.FindByPhoneNumber(Ports, e.FromNumber);
        //     
        //     if (fromPort?.Status == PortStatus.InCall)
        //     {
        //         fromPort?.Terminal.EndCall();
        //     }
        //     
        //     if (targetPort?.Status == PortStatus.InCall)
        //     {
        //         targetPort?.Terminal.EndCall();
        //     }
        // }
    }
}
