using System;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities.ATE
{
    public class Terminal : ITerminal
    {
        #region Events

        public event EventHandler<TerminalArgs> ConnectedEvent;
        public event EventHandler<TerminalArgs> DisconnectedEvent;
        public event EventHandler<CallArgs> CallEvent;
        public event EventHandler<CallArgs> IncomingCallEvent;
        public event EventHandler<CallArgs> CallAcceptedEvent;
        public event EventHandler<CallArgs> CallEndedEvent;
        public event EventHandler<CallArgs> CallRejectedEvent;
        
        #endregion

        #region Props

        public IPort Port { get; protected set; }
        public IContract Contract { get; }
        public string Number => Contract.PhoneNumber;
        public Call CurrentCall { get; private set; }
        
        #endregion

        #region Ctors

        public Terminal(IContract contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("Договор не может быть null");
            }

            Contract = contract;
        }

        #endregion

        #region Methods

        public void ConnectTo(AutomaticTelephoneExchange ate)
        {
            if (Port != null)
            {
                throw new Exception("Терминал уже подключен к АТС");
            }

            Port = ate.Connect(this);

            RaiseTerminalConnectedEvent();
        }

        public void Disconnect()
        {
            if (Port == null)
            {
                throw new Exception("Терминал не подключен к АТС");
            }
            
            RaiseTerminalDisconnectedEvent();

            Port = null;
            
        }

        public void CallTo(string targetNumber)
        {
            if (Port.Status == PortStatus.InCall)
            {
                throw new Exception("Терминал уже находится в состоянии вызова");
            }

            CurrentCall = new Call(Number, targetNumber);
            RaiseCallEvent(CurrentCall);
        }
        
        public void HandleIncomingCall(Call call)
        {
            CurrentCall = call;
            RaiseIncomingCallEvent(call);
        }

        public void AcceptIncomingCall()
        {
            CurrentCall.Accept();
            RaiseCallAcceptedEvent(CurrentCall);
        }

        public void RejectIncomingCall()
        {
            CurrentCall.Reject();
            RaiseRejectedEvent(CurrentCall);
        }
        
        public void EndCall()
        {
            CurrentCall.End();
            RaiseCallEndedEvent(CurrentCall);
        }
        #endregion
        
        #region RaiseEvents

        private void RaiseTerminalConnectedEvent()
        {
            var args = new TerminalArgs(this);
            ConnectedEvent?.Invoke(this, args);
        }

        private void RaiseTerminalDisconnectedEvent()
        {
            var args = new TerminalArgs(this);
            DisconnectedEvent?.Invoke(this, args);
        }

        private void RaiseCallEvent(Call call)
        {
            CallEvent?.Invoke(this, new CallArgs(call));
        }

        private void RaiseIncomingCallEvent(Call call)
        {
            IncomingCallEvent?.Invoke(this, new CallArgs(call));
        }

        private void RaiseCallEndedEvent(Call call)
        {
            CallEndedEvent?.Invoke(this, new CallArgs(call));
        }

        private void RaiseCallAcceptedEvent(Call call)
        {
            CallAcceptedEvent?.Invoke(this, new CallArgs(call));
        }

        private void RaiseRejectedEvent(Call call)
        {
            CallRejectedEvent?.Invoke(this, new CallArgs(call));
        }

        #endregion
    }
}