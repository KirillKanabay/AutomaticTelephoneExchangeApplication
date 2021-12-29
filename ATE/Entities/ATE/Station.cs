using System;
using ATE.Args;
using ATE.Entities.Billings;
using ATE.Entities.Port;
using ATE.Entities.Terminal;
using ATE.Enums;

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
                if (!port.IsConnectedToStation)
                {
                    port.ConnectToStation(this);
                }
                port.ConnectToTerminal(terminal);
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }

        public override void SubscribeToBillingSystem(BaseBillingSystem billingSystem)
        {
            billingSystem.CallAllowedEvent += OnCallAllowed;
            billingSystem.CallCanceledEvent += OnCallCanceled;
        }
        public override void OnTerminalStartingCall(object sender, CallArgs e)
        {
            OnCallStartedEvent(sender, e);
        }
        public override void OnTerminalAcceptingCall(object sender, CallArgs e)
        {
            var port = _portController.GetByPhoneNumber(e.FromNumber);
            port.HandleOutgoingAcceptedCall(this, e);
        }
        public override void OnTerminalRejectingCall(object sender, CallArgs e)
        {
            var port = _portController.GetByPhoneNumber(e.FromNumber);
            if (port != null)
            {
                port.HandleOutgoingAcceptedCall(sender, e);
            }
        }
        public override void OnTerminalEndingCall(object sender, CallArgs e)
        {
            if (sender is BaseTerminal terminal && e.Status == CallStatus.Accepted)
            {
                BasePort port = null;

                if (terminal.Number == e.FromNumber)
                {
                    port = _portController.GetByPhoneNumber(e.TargetNumber);
                }
                else
                {
                    port = _portController.GetByPhoneNumber(e.FromNumber);
                }

                e.Status = CallStatus.Ended;

                port.HandleEndedCall(sender, e);

                OnCallEndedEvent(sender, e);
            }
        }
        public override void OnCallAllowed(object sender, CallArgs e)
        {
            var port = _portController.GetByPhoneNumber(e.TargetNumber);
            if (port == null)
            {
                throw new ArgumentException("Неправильно набран номер");
            }

            port.HandleIncomingCall(sender, e);

        }
        public override void OnCallCanceled(object sender, CallCanceledArgs e)
        {
            var port = _portController.GetByPhoneNumber(e?.SourcePhoneNumber);

            port.HandleCanceledCall(sender, e);
        }

    }
}
