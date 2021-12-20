using System;
using ATE.Core.Entities.ATE;
using ATE.Core.Enums;

namespace ATE.Core.Args
{
    public class CallArgs : EventArgs
    {
        public Call Call { get; }
        
        public string TargetNumber => Call.TargetNumber;
        public string FromNumber => Call.FromNumber;
        public CallStatus Status => Call.Status;

        public DateTime Date => Call.Date;
        public DateTime? StartDate => Call.StartDate;
        public DateTime? EndDate => Call.EndDate;

        public double DurationInMinutes => Call.DurationInMinutes;

        public CallArgs(Call call)
        {
            Call = call;
        }
    }
}