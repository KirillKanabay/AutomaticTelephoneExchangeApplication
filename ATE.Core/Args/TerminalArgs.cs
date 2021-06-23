using System;
using ATE.Core.Interfaces;

namespace ATE.Core.Args
{
    public class TerminalArgs : EventArgs
    {
        public ITerminal FromTerminal { get; }
        public string TargetNumber { get; }

        public TerminalArgs(ITerminal fromTerminal, string targetNumber)
        {
            FromTerminal = fromTerminal;
            TargetNumber = targetNumber;
        }
    }
}