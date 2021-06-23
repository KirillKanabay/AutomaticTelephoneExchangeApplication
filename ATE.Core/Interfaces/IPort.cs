using ATE.Core.Args;
using ATE.Core.Enums;

namespace ATE.Core.Interfaces
{
    public interface IPort
    {
        int PortNumber { get; }
        PortStatus Status { get; set; }

        void OnTerminalCall(object sender, TerminalArgs e);
    }
}