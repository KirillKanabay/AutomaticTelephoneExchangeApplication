using System;
using ATE.Enums;

namespace ATE.Args
{
    public class CallArgs : EventArgs
    {
        public string FromNumber { get; set; }
        public string TargetNumber { get; set; }
        public CallStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}