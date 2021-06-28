using System;
using ATE.Core.Entities;
using ATE.Core.Entities.ATE;
using ATE.Core.Interfaces;

namespace ATE.Core.Args
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