using System;
using ATE.Core.Interfaces.ATE;

namespace ATE.Args
{
    public class TerminalArgs : EventArgs
    {
        public ITerminal Terminal { get; }

        public TerminalArgs(ITerminal terminal)
        {
            Terminal = terminal;
        }
    }
}