using System;
using ATE.Entities.ATE;
using ATE.Enums;

namespace ATE.Entities.Billings
{
    public class CallInformation
    {
        public readonly string ClientPhoneNumber;
        public readonly string DestinationPhoneNumber;
        public readonly decimal Price;
        public readonly double Duration;
        public readonly DateTime CallDate;
        public readonly CallType CallType;

        public CallInformation(Call call, decimal pricePerMinuteCall, CallType callType)
        {
            CallType = callType;
            if (CallType == CallType.Incoming)
            {
                ClientPhoneNumber = call.TargetNumber;
                DestinationPhoneNumber = call.FromNumber;
            }
            else
            {
                ClientPhoneNumber = call.FromNumber;
                DestinationPhoneNumber = call.TargetNumber;
            }
            
            Duration = call.DurationInMinutes;
            Price = CallType == CallType.Outgoing ? CalculateCallPrice(Duration, pricePerMinuteCall) : 0;
            CallDate = call.Date;
        }
        
        private decimal CalculateCallPrice(double durationInMinutes, decimal pricePerMinuteCall)
        {
            decimal price = Math.Ceiling(Convert.ToDecimal(durationInMinutes)) * pricePerMinuteCall;
            return price;
        }
    }
}