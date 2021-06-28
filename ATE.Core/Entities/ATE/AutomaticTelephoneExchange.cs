﻿using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.ATE;

namespace ATE.Core.Entities.ATE
{
    public class AutomaticTelephoneExchange: IAutomaticTelephoneExchange
    {
        private readonly ICompany _company;
        
        private ICollection<IPort> Ports { get; set; }
        
        public AutomaticTelephoneExchange(ICompany company, int portCount)
        {
            _company = company;
            if (portCount <= 0)
            {
                throw new ArgumentException("Количество портов станции не может быть меньше или равно нулю.");
            }
            InitPorts(portCount);
        }

        public IPort Connect(ITerminal terminal)
        {
            var port =  Ports.FirstOrDefault(p => p.Terminal == null);
            if (port != null)
            {
                port.ConnectTerminal(terminal);
                SubscribeToTerminalEvents(terminal);
            }
            else
            {
                throw new Exception("Все доступные порты станции заняты. Попробуйте позже!");
            }
            return port;
        }
        
        private void InitPorts(int portCount)
        {
            Ports = new List<IPort>();
            for (var portNumber = 1; portNumber <= portCount; portNumber++)
            {
                var port = new Port(portNumber);
                Ports.Add(port);
            }
        }

        private void SubscribeToTerminalEvents(ITerminal terminal)
        {
            terminal.CallEvent += OnTerminalCall;
            terminal.DisconnectedEvent += OnTerminalDisconnected;
        }

        private void UnsubscribeFromTerminalEvents(ITerminal terminal)
        {
            terminal.CallEvent -= OnTerminalCall;
            terminal.DisconnectedEvent -= OnTerminalDisconnected;
        }

        private void SubscribeToTerminalCallEvents(ITerminal terminal)
        {
            terminal.CallRejectedEvent += OnTerminalRejectedCall;
            terminal.CallEndedEvent += OnTerminalCallEnded;
        }

        private void UnsubscribeFromTerminalCallEvents(ITerminal terminal)
        {
            terminal.CallRejectedEvent -= OnTerminalRejectedCall;
            terminal.CallEndedEvent -= OnTerminalCallEnded;
        }
        
        private void OnTerminalDisconnected(object sender, TerminalArgs e)
        {
            UnsubscribeFromTerminalEvents(e.Terminal);
        }
        
        private void OnTerminalCall(object sender, CallArgs e)
        {
            ITerminal targetTerminal = FindPort(e.TargetNumber)?.Terminal;

            if (targetTerminal == null)
            {
                throw new Exception("Неправильно набран номер");
            }

            if (targetTerminal.Port.Status == PortStatus.InCall)
            {
                throw new Exception("Абонент занят. Перезвоните позже.");
            }
            
            targetTerminal.HandleIncomingCall(e.Call);
        }

        private void OnTerminalRejectedCall(object sender, CallArgs e)
        {
            ITerminal fromTerminal = Ports.FirstOrDefault(t => t.Terminal?.Number == e.Call.FromNumber)?.Terminal;
            fromTerminal?.ResetCall();
        }

        private void OnTerminalCallEnded(object sender, CallArgs e)
        {
            IPort targetPort = FindPort(e.TargetNumber);
            IPort fromPort = FindPort(e.FromNumber);
            
            if (fromPort?.Status == PortStatus.InCall)
            {
                fromPort?.Terminal.EndCall();
            }
            if (targetPort?.Status == PortStatus.InCall)
            {
                targetPort?.Terminal.EndCall();
            }
        }

        private IPort FindPort(string phoneNumber)
        {
            return Ports.FirstOrDefault(t => t.Terminal?.Number == phoneNumber);
        }
    }
}
