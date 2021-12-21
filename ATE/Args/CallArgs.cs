using System;
using ATE.Entities.ATE;
using ATE.Enums;

namespace ATE.Args
{
    public class CallArgs : EventArgs
    {
        public Call Call { get; set; }
        
        public string TargetNumber => Call.TargetNumber;
        public string FromNumber => Call.FromNumber;
        public CallStatus Status => Call.Status;

        public DateTime Date => Call.Date;
        public DateTime? StartDate => Call.StartDate;
        public DateTime? EndDate => Call.EndDate;

        public double DurationInMinutes => Call.DurationInMinutes;
    }
}