using System;
using ATE.Core.Args;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public abstract class BaseTerminal : ITerminal
    {
        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<CallArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        
        public string Number => Contract.PhoneNumber;
        public IPort Port { get; protected set; }
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
            
            RaiseTerminalConnectedEvent();
        }

        public virtual void Disconnect()
        {
            if (Port == null)
            {
                throw new Exception("Телефон не подключен к АТС");
            }
            
            RaiseTerminalDisconnectedEvent();
            
            Port = null;
        }
        
        public abstract void CallTo(string targetNumber);
        public abstract void HandleIncomingCall(Call call);
        public abstract void AcceptIncomingCall(Call call);
        public abstract void RejectIncomingCall(Call call);
        public abstract void EndCall(Call call);

        protected void RaiseTerminalConnectedEvent()
        {
            var args = new TerminalArgs(this);
            ConnectedEvent?.Invoke(this, args);
        }
        protected void RaiseTerminalDisconnectedEvent()
        {
            var args = new TerminalArgs(this);
            DisconnectedEvent?.Invoke(this, args);
        }
        protected void RaiseCallEvent(string targetNumber)
        {
            var args = new CallArgs(new Call(Number, targetNumber));
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