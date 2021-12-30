using ATE.Args;
using ATE.Entities.Calls;
using ATE.Entities.Company;
using ATE.Entities.Users;
using ATE.Enums;

namespace ATE.Mapper
{
    public static class CallMapper
    {
        public static Call MapToCall(CallArgs callArgs)
        {
            return new Call()
            {
                Date = callArgs.Date,
                EndDate = callArgs.EndDate,
                FromNumber = callArgs.SourceNumber,
                StartDate = callArgs.StartDate,
                TargetNumber = callArgs.TargetNumber,
                Status = callArgs.Status,
            };
        }

        public static CallArgs MapToArgs(Call call)
        {
            return new CallArgs()
            {
                Date = call.Date,
                StartDate = call.StartDate,
                EndDate = call.EndDate,
                SourceNumber = call.FromNumber,
                Status = call.Status,
                TargetNumber = call.TargetNumber
            };
        }

        public static CallInformation MapToCallInformation(CallArgs callArgs, Client client)
        {
            CallType callType = client.PhoneNumber == callArgs.SourceNumber ? CallType.Outgoing : CallType.Incoming;

            CallInformation callInformation = new CallInformation()
            {
                Client = client,
                DestinationPhoneNumber = callType == CallType.Incoming ? callArgs.SourceNumber : callArgs.TargetNumber,
                Date = callArgs.Date,
                StartDate = callArgs.StartDate,
                EndDate = callArgs.EndDate,
                Type = callType,
            };

            return callInformation;
        }
    }
}
