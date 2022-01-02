using System;
using ATE.Abstractions.Domain.Terminal;

namespace ATE.Args
{
    public class TerminalArgs : EventArgs
    {
        public BaseTerminal Terminal { get; }

        public TerminalArgs(BaseTerminal terminal)
        {
            Terminal = terminal;
        }
    }
}