using ATE.Core.Enums;
using ATE.Core.Interfaces;

namespace ATE.Core.Entities
{
    public class Port : IPort
    {
        public int PortNumber { get; }
        public PortStatus Status { get; }

        public Port(int portNumber, PortStatus status)
        {
            PortNumber = portNumber;
            Status = status;
        }

        public override int GetHashCode()
        {
            return PortNumber.GetHashCode();
        }
    }
}