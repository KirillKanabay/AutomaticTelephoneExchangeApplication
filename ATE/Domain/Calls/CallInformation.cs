using System;
using ATE.Domain.Users;
using ATE.Enums;

namespace ATE.Domain.Calls
{
    public class CallInformation
    {
        public Client Client { get; set; }
        public string DestinationPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public CallType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DurationInMinutes => (EndDate - StartDate).TotalMinutes;

    }
}