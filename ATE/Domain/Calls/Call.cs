﻿using System;
using ATE.Enums;

namespace ATE.Domain.Calls
{
    public class Call
    {
        public string FromNumber { get; set; }
        public string TargetNumber { get; set; }
        public CallStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public void Accept()
        {
            Status = CallStatus.Accepted;
            StartDate = DateTime.Now;
        }

        public void End()
        {
            Status = CallStatus.Ended;
            EndDate = DateTime.Now;
        }
        
        public void Reject()
        {
            Status = CallStatus.Rejected;
        }
    }
}