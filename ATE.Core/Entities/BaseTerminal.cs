using System;
using ATE.Core.Args;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public abstract class BaseTerminal : ITerminal
    {
        public event EventHandler<TerminalArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        public string Number => Contract.PhoneNumber;
        public Port Port { get; protected set; }
        public Contract Contract { get; }

        protected BaseTerminal(Contract contract)
        {
            Contract = contract;
        }
        
        public virtual void ConnectTo(AutomaticTelephoneExchange ate)
        {
            if (Port != null)
            {
                throw new Exception("Терминал уже подключен к АТС");
            }
            Port = ate.Connect(this);
        }

        public virtual void Disconnect()
        {
            if (Port == null)
            {
                throw new Exception("Телефон не подключен к АТС");
            }
            
            Port.Disconnect();
        }
        
        public abstract void CallTo(string number);
        public abstract void HandleIncomingCall(Call call);
        public abstract void AcceptIncomingCall(Call call);
        public abstract void RejectIncomingCall(Call call);
        

        protected void RaiseCallEvent(string number)
        {
            var args = new TerminalArgs(this, number);
            CallEvent?.Invoke(this, args);
        }
        
        protected void RaiseIncomingCallEvent(Call call) //todo: Сделать Call интерфейсом
        {
            var args = new CallArgs(call);
            IncomingCallEvent?.Invoke(this, args);
        }
        
        protected void RaiseCallEndedEvent(Call call)
        {
            var args = new CallArgs(call);
            CallEndedEvent?.Invoke(this, args);
        }
        
        protected void RaiseCallAcceptedEvent(Call call)
        {
            var args = new CallArgs(call);
            CallAcceptedEvent?.Invoke(this, args);
        }
        
        protected void RaiseRejectedEvent(Call call)
        {
            var args = new CallArgs(call);
            CallRejectedEvent?.Invoke(this, args);
        }
    }
}