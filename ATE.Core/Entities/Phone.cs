using System;
using ATE.Core.Args;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Phone : ITerminal
    {
        
        private readonly Contract _contract;
        public Port Port { get; private set; }
        public Client Client => _contract.Client;
        public string Number => _contract.PhoneNumber;
        
        #region Events
        public event EventHandler<TerminalArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        #endregion
        
        public Phone(Contract contract)
        {
            _contract = contract;
        }
        
        public void ConnectToPort(AutomaticTelephoneExchange ate)
        {
            if (Port != null)
            {
                throw new Exception("Телефон уже подключен к АТС");
            }
            Port = ate.Connect(this);
        }

        public void CallTo(string number)
        {
            var args = new TerminalArgs(this, number);
            CallEvent?.Invoke(this, args);
        }
        
        public void HandleIncomingCall(Call call)
        {
            var args = new CallArgs(call);
            IncomingCallEvent?.Invoke(this, args);
        }

        public void AcceptIncomingCall(Call call)
        {
            var args = new CallArgs(call);
            CallAcceptedEvent?.Invoke(this, args);
        }

        public void RejectIncomingCall(Call call)
        {
            var args = new CallArgs(call);
            CallRejectedEvent?.Invoke(this, args);
        }
    }
}