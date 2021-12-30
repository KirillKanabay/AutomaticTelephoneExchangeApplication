using System;
using ATE.Args;
using ATE.Helpers;

namespace ATE.Entities.Terminal
{
    public class TerminalView
    {
        private readonly BaseTerminal _terminal;

        public TerminalView(BaseTerminal terminal)
        {
            _terminal = terminal;
            SubscribeToTerminal();
        }

        public void SubscribeToTerminal()
        {
            _terminal.ConnectedEvent += OnTerminalConnected;
            _terminal.OutgoingCallEvent += OnCall;
            _terminal.IncomingCallEvent += OnIncomingCall;
            _terminal.CallAcceptedEvent += OnCallAccepted;
            _terminal.CallRejectedEvent += OnCallRejected;
            _terminal.CallEndedEvent += OnCallEnded;
            _terminal.DisconnectedEvent += OnTerminalDisconnected;
            _terminal.CallCanceledEvent += OnCallCanceled;
        }

        public void UnsubscribeFromTerminal()
        {
            _terminal.ConnectedEvent -= OnTerminalConnected;
            _terminal.OutgoingCallEvent -= OnCall;
            _terminal.IncomingCallEvent -= OnIncomingCall;
            _terminal.CallAcceptedEvent -= OnCallAccepted;
            _terminal.CallRejectedEvent -= OnCallRejected;
            _terminal.CallEndedEvent -= OnCallEnded;
            _terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }

        protected virtual void OnCall(object sender, CallArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"Calling to: {e.TargetNumber}\n",
                ConsoleColor.Green);
        }
        protected virtual void OnIncomingCall(object sender, CallArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"Incoming call from {e.SourceNumber}\n",
                ConsoleColor.Green);
            if (ConsoleEx.CheckContinue("Accept call?([Y]es/[N]o):"))
            {
                Console.WriteLine();
                _terminal.AcceptCall();
            }
            else
            {
                Console.WriteLine();
                _terminal.RejectCall();
            }
        }
        protected virtual void OnCallAccepted(object sender, CallArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"Call with {e.SourceNumber} accepted\n",
                ConsoleColor.Green);
        }
        protected virtual void OnCallRejected(object sender, CallArgs e)
        {
            WriteTerminalNumber();
            if (_terminal.Number == e.TargetNumber)
            {
                ConsoleEx.WriteLineWithColor($"Call with {e.SourceNumber} rejected\n",
                    ConsoleColor.Red);
            }
            else
            {
                ConsoleEx.WriteLineWithColor($"Call with {e.TargetNumber} rejected\n",
                    ConsoleColor.Red);
            }
        }
        protected virtual void OnCallEnded(object sender, CallArgs e)
        {
            string targetNumber = _terminal.Number == e.SourceNumber ? e.TargetNumber : e.SourceNumber;

            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"Call with {targetNumber} ended\n",
                ConsoleColor.Gray);
        }
        protected virtual void OnTerminalConnected(object sender, TerminalArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"successfully connected to station", ConsoleColor.DarkMagenta);
        }
        protected virtual void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineWithColor($"disconnected from station\n", ConsoleColor.DarkMagenta);
        }
        protected virtual void OnCallCanceled(object sender, CallCanceledArgs e)
        {
            WriteTerminalNumber();
            ConsoleEx.WriteLineError($"Call error: {e.Message}\n");
        }

        private void WriteTerminalNumber()
        {
            ConsoleEx.WriteWithColor($"[{_terminal.Number}]:", ConsoleColor.White, ConsoleColor.Blue);
            Console.Write(" ");
        }
    }
}