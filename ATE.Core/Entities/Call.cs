using System;
using ATE.Core.Args;
using ATE.Core.Enums;

namespace ATE.Core.Entities
{
    public class Call
    {
        public CallStatus Status { get; private set; } = CallStatus.Waiting;
        public string FromNumber { get; private set; }
        public string TargetNumber { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public Call(string fromNumber, string targetNumber)
        {
            FromNumber = fromNumber;
            TargetNumber = targetNumber;
        }
        
        public void OnTerminalCallingAccepted(object sender, CallArgs e)
        {
            Status = CallStatus.Accepted;
            StartTime = DateTime.UtcNow;
        }

        public void OnTerminalCallingEnded(object sender, TerminalArgs e)
        {
            EndTime = DateTime.Now;
        }

        public void OnTerminalCallingRejected(object sender, CallArgs e)
        {
            Status = CallStatus.Rejected;
            StartTime = DateTime.Now;
            EndTime = null;
        }
    }
}