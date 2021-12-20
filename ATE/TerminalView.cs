using System;
using ATE.Args;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Helpers;

namespace ATE
{
    public class TerminalView : ITerminalSubscriber
    {
        private readonly ITerminal _terminal;

        public TerminalView(ITerminal terminal)
        {
            _terminal = terminal;
            SubscribeToTerminal(_terminal);
        }
        
        public void SubscribeToTerminal(ITerminalObserver terminal)
        {
            _terminal.ConnectedEvent += OnTerminalConnected;
            _terminal.CallEvent += OnCall;
            _terminal.IncomingCallEvent += OnIncomingCall;
            _terminal.CallAcceptedEvent += OnCallAccepted;
            _terminal.CallRejectedEvent += OnCallRejected;
            _terminal.CallEndedEvent += OnCallEnded;
            _terminal.DisconnectedEvent += OnTerminalDisconnected;
        }

        public void UnsubscribeFromTerminal(ITerminalObserver terminal)
        {
            _terminal.ConnectedEvent -= OnTerminalConnected;
            _terminal.CallEvent -= OnCall;
            _terminal.IncomingCallEvent -= OnIncomingCall;
            _terminal.CallAcceptedEvent -= OnCallAccepted;
            _terminal.CallRejectedEvent -= OnCallRejected;
            _terminal.CallEndedEvent -= OnCallEnded;
            _terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }
        
        public void OnCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Происходит вызов номера: {e.TargetNumber}", ConsoleColor.Green);
        }
        
        public void OnIncomingCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Входящий вызов от {e.FromNumber}",
                ConsoleColor.Green);
            if (ConsoleEx.CheckContinue("Принять вызов?([Y]es/[N]o):"))
            {
                _terminal.AcceptIncomingCall();
            }
            else
            {
                _terminal.RejectCall();
            }
        }

        public void OnCallAccepted(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок от {e.FromNumber} был принят", ConsoleColor.Green);
        }

        public void OnCallRejected(object sender, CallArgs e)
        {
            if (_terminal.Number == e.TargetNumber)
            {
                ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {e.FromNumber} был отклонен", ConsoleColor.Red);
            }
            else
            {
                ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {e.TargetNumber} был отклонен", ConsoleColor.Red);
            }
        }

        public void OnCallEnded(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Звонок с {e.TargetNumber} был завершен", ConsoleColor.DarkGreen);
        }

        public void OnTerminalConnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] Был подключен к АТС", ConsoleColor.DarkMagenta);
        }
        
        public void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}] отключен от станции АТС", ConsoleColor.DarkMagenta);
        }
    }
}