using System;
using ATE.Entities.ATE;
using ATE.Entities.Company;
using ATE.Enums;

namespace ATE.Entities.Billings
{
    public class CallInformation
    {
        public Client Source { get; set; }
        public Client Destination { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public DateTime CallDate { get; set; }
        public CallType CallType { get; set; }

        // public CallInformation(Call call, decimal pricePerMinuteCall, CallType callType)
        // {
        //     CallType = callType;
        //     if (CallType == CallType.Incoming)
        //     {
        //         ClientPhoneNumber = call.TargetNumber;
        //         DestinationPhoneNumber = call.FromNumber;
        //     }
        //     else
        //     {
        //         ClientPhoneNumber = call.FromNumber;
        //         DestinationPhoneNumber = call.TargetNumber;
        //     }
        //     
        //     Duration = call.DurationInMinutes;
        //     Price = CallType == CallType.Outgoing ? CalculateCallPrice(Duration, pricePerMinuteCall) : 0;
        //     CallDate = call.Date;
        // }
        //
        // private decimal CalculateCallPrice(double durationInMinutes, decimal pricePerMinuteCall)
        // {
        //     decimal price = Math.Ceiling(Convert.ToDecimal(durationInMinutes)) * pricePerMinuteCall;
        //     return price;
        // }
    }
}