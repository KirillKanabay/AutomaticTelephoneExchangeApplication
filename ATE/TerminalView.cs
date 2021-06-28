using System;
using ATE.Core.Args;
using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;
using ATE.Helpers;

namespace ATE
{
    public class TerminalView
    {
        private readonly ITerminal _terminal;

        public TerminalView(ITerminal terminal)
        {
            _terminal = terminal;
            _terminal.ConnectedEvent += OnTerminalConnected;
            _terminal.CallEvent += OnCall;
            _terminal.IncomingCallEvent += OnIncomingCall;
            _terminal.CallAcceptedEvent += OnCallAccepted;
            _terminal.CallRejectedEvent += OnCallRejected;
            _terminal.CallEndedEvent += OnCallEnded;
            _terminal.DisconnectedEvent += OnTerminalDisconnected;
        }

        public void OnCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Происходит вызов номера: {e.TargetNumber}", ConsoleColor.Green);
        }
        
        public void OnIncomingCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}]: Входящий вызов от {e.FromNumber}",
                ConsoleColor.Green);
            if (ConsoleEx.CheckContinue("Принять вызов?([Y]es/[N]o):"))
            {
                _terminal.AcceptIncomingCall();
            }
            else
            {
                _terminal.RejectIncomingCall();
            }
        }

        public void OnCallAccepted(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}] Звонок от {e.FromNumber} был принят", ConsoleColor.Green);
        }

        public void OnCallRejected(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}] Звонок с {e.TargetNumber} был отклонен", ConsoleColor.Red);
        }

        public void OnCallEnded(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}] Звонок с {e.TargetNumber} был завершен", ConsoleColor.DarkGreen);
        }

        public void OnTerminalConnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}] Был подключен к АТС", ConsoleColor.DarkMagenta);
        }
        
        public void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"\n[{_terminal.Number}] отключен от станции АТС", ConsoleColor.DarkMagenta);
        }
    }
}