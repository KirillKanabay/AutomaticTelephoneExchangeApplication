using ATE.Core.Args;
using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Port : IPort
    {
        public int PortNumber { get; }
        public PortStatus Status { get; set; }

        public Port(int portNumber, PortStatus status)
        {
            PortNumber = portNumber;
            Status = status;
        }
        
        public void OnTerminalCall(object sender, TerminalArgs e)
        {
            Status = PortStatus.InCall;
        }

        public void OnTerminalEndCall(object sender, TerminalArgs e)
        {
            Status = PortStatus.Connected;
        }

        public void Disconnect()
        {
            //TODO: Реализовать отключение порта
        }
        public override int GetHashCode()
        {
            return PortNumber.GetHashCode();
        }
    }
}