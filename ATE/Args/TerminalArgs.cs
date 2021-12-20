using System;
using ATE.Core.Interfaces.ATE;
using ATE.Entities.Terminal;

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