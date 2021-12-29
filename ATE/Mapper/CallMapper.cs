using ATE.Args;
using ATE.Entities.ATE;

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
                FromNumber = callArgs.FromNumber,
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
                FromNumber = call.FromNumber,
                Status = call.Status,
                TargetNumber = call.TargetNumber
            };
        }
    }
}
