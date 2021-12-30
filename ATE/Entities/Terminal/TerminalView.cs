using System;
using ATE.Args;
using ATE.Helpers;

namespace ATE.Entities.Terminal
{
    public class TerminalView
    {
        private readonly BaseTerminal _terminal;

        public TerminalView(BaseTerminal terminal)
        {
            _terminal = terminal;
            SubscribeToTerminal();
        }

        public void SubscribeToTerminal()
        {
            _terminal.ConnectedEvent += OnTerminalConnected;
            _terminal.OutgoingCallEvent += OnCall;
            _terminal.IncomingCallEvent += OnIncomingCall;
            _terminal.CallAcceptedEvent += OnCallAccepted;
            _terminal.IncomingRejectedCallEvent += OnCallRejected;
            _terminal.CallEndedEvent += OnCallEnded;
            _terminal.DisconnectedEvent += OnTerminalDisconnected;
            _terminal.CallCanceledEvent += OnCallCanceled;
        }

        public void UnsubscribeFromTerminal()
        {
            _terminal.ConnectedEvent -= OnTerminalConnected;
            _terminal.OutgoingCallEvent -= OnCall;
            _terminal.IncomingCallEvent -= OnIncomingCall;
            _terminal.CallAcceptedEvent -= OnCallAccepted;
            _terminal.IncomingRejectedCallEvent -= OnCallRejected;
            _terminal.CallEndedEvent -= OnCallEnded;
            _terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }

        protected virtual void OnCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Происходит вызов номера: {e.TargetNumber}\n",
                ConsoleColor.Green);
        }
        protected virtual void OnIncomingCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Входящий вызов от {e.FromNumber}\n",
                ConsoleColor.Green);
            if (ConsoleEx.CheckContinue("Принять вызов?([Y]es/[N]o):"))
            {
                _terminal.AcceptCall();
            }
            else
            {
                _terminal.RejectCall();
            }
        }
        protected virtual void OnCallAccepted(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок от {e.FromNumber} был принят\n",
                ConsoleColor.Green);
        }
        protected virtual void OnCallRejected(object sender, CallArgs e)
        {
            if (_terminal.Number == e.TargetNumber)
            {
                ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {e.FromNumber} был отклонен\n",
                    ConsoleColor.Red);
            }
            else
            {
                ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {e.TargetNumber} был отклонен\n",
                    ConsoleColor.Red);
            }
        }
        protected virtual void OnCallEnded(object sender, CallArgs e)
        {
            var targetNumber = String.Empty;

            if (_terminal.Number == e.FromNumber)
            {
                targetNumber = e.TargetNumber;
            }
            else
            {
                targetNumber = e.FromNumber;
            }


            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {targetNumber} был завершен\n",
                ConsoleColor.Gray);
        }
        protected virtual void OnTerminalConnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Был подключен к АТС", ConsoleColor.DarkMagenta);
        }
        protected virtual void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] отключен от станции АТС\n", ConsoleColor.DarkMagenta);
        }
        protected virtual void OnCallCanceled(object sender, CallCanceledArgs e)
        {
            ConsoleEx.WriteLineError($"[{_terminal.Number}] Call error: {e.Message}\n");
        }
    }
}