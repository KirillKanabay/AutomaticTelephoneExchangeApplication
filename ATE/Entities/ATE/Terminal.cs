using System;
using ATE.Args;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;
using ATE.Enums;

namespace ATE.Entities.ATE
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
            Contract = contract ?? throw new ArgumentNullException(nameof(contract), "Договор не может быть null");
        }

        #endregion

        #region Methods

        public void ConnectTo(IAutomaticTelephoneExchange ate)
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
        
        public void ResetCall()
        {
            CurrentCall = null;
        }
        
        public void HandleIncomingCall(Call call)
        {
            CurrentCall = call;
            RaiseIncomingCallEvent(CurrentCall);
        }

        public void AcceptIncomingCall()
        {
            CurrentCall.Accept();
            RaiseCallAcceptedEvent(CurrentCall);
        }

        public void RejectCall()
        {
            CurrentCall.Reject();
            RaiseCallRejectedEvent(CurrentCall);
            ResetCall();
        }
        
        public void EndCall()
        {
            CurrentCall.End();
            RaiseCallEndedEvent(CurrentCall);
            ResetCall();
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

        private void RaiseCallRejectedEvent(Call call)
        {
            CallRejectedEvent?.Invoke(this, new CallArgs(call));
        }
        
        #endregion
    }
}