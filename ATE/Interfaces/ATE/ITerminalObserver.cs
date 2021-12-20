using System;
using ATE.Core.Args;

namespace ATE.Core.Interfaces.ATE
{
    public interface ITerminalObserver
    {
        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        public event EventHandler<CallArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
    }
}