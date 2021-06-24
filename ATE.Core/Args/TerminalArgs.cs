using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;

namespace ATE.Core.Args
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