using System;
using ATE.Core.Args;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Helpers;

namespace ATE.Views.Terminals
{
    public class TerminalView
    {
        private readonly BaseTerminal _terminal;

        public TerminalView(BaseTerminal terminal)
        {
            _terminal = terminal;
            _terminal.CallEvent += OnCall;
            _terminal.IncomingCallEvent += OnIncomingCall;
            _terminal.CallAcceptedEvent += OnCallAccepted;
            _terminal.CallRejectedEvent += OnCallRejected;
        }

        public void OnCall(object sender, TerminalArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Происходит вызов номера: {e.TargetNumber}", ConsoleColor.Green);
        }
        
        public void OnIncomingCall(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Входящий вызов от {e.Call.FromNumber}",
                ConsoleColor.Green);
            if (ConsoleEx.CheckContinue("Принять вызов?([Y]es/[N]o):"))
            {
                _terminal.AcceptIncomingCall(e.Call);
            }
            else
            {
                _terminal.RejectIncomingCall(e.Call);
            }
        }

        public void OnCallAccepted(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Вызов от {e.Call.FromNumber} был принят", ConsoleColor.Green);
        }

        public void OnCallRejected(object sender, CallArgs e)
        {
            ConsoleEx.WriteLineWithColor($"[{_terminal.Number}]: Звонок с {e.Call.TargetNumber} был отклонен", ConsoleColor.Red);
        }
        
    }
}