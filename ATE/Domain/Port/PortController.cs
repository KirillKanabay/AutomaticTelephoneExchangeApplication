﻿using System;
using System.Collections.Generic;
using System.Linq;
using ATE.Abstractions.Domain.Port;
using ATE.Enums;

namespace ATE.Domain.Port
{
    public class PortController : IPortController
    {
        private readonly IEnumerable<BasePort> _ports;

        public PortController(int portsCount)
        {
            _ports = InitPorts(portsCount);
        }

        public BasePort GetAvailablePort()
        {
            return _ports.FirstOrDefault(p => p.Status == PortStatus.Available);
        }

        public BasePort GetByPhoneNumber(string phoneNumber)
        {
            return _ports.FirstOrDefault(p => p?.CurrentTerminal?.
                Number == phoneNumber);
        }

        private IEnumerable<BasePort> InitPorts(int portsCount)
        {
            if (portsCount < 1)
            {
                throw new ArgumentException("Ports count can't be less than 1");
            }

            var ports = new List<BasePort>();
            for (var portNumber = 1; portNumber <= portsCount; portNumber++)
            {
                var port = new Domain.Port.Port(portNumber);
                ports.Add(port);
            }

            return ports;
        }

    }
}
