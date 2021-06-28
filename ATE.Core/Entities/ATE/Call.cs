using System;
using ATE.Core.Enums;

namespace ATE.Core.Entities.ATE
{
    public class Call
    {
        public string FromNumber { get; }
        public string TargetNumber { get; }
        public CallStatus Status { get; private set; }
        
        public DateTime Date { get; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public double DurationInMinutes => (EndDate - StartDate)?.TotalMinutes ?? 0;
        
        public Call(string fromNumber, string targetNumber)
        {
            FromNumber = fromNumber;
            TargetNumber = targetNumber;
            
            Date = DateTime.Now;
        }

        public void Accept()
        {
            Status = CallStatus.Accepted;
            StartDate = DateTime.Now;
        }

        public void End()
        {
            EndDate = DateTime.Now;
        }
        
        public void Reject()
        {
            Status = CallStatus.Rejected;
        }
    }
}