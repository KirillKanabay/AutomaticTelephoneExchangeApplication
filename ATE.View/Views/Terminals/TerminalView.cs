using System;
using ATE.Core.Args;
using ATE.Core.Interfaces;
using ATE.Helpers;

namespace ATE.Views.Terminals
{
    public class TerminalView
    {
        private readonly ITerminal _terminal;

        public TerminalView(ITerminal terminal)
        {
            _terminal = terminal;
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
    }
}