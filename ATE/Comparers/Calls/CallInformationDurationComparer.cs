using System.Collections.Generic;
using ATE.Entities.ATE;
using ATE.Entities.Calls;

namespace ATE.Comparers.Calls
{
    public class CallInformationDurationComparer : IComparer<CallInformation>
    {
        public int Compare(CallInformation x, CallInformation y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            return x.DurationInMinutes.CompareTo(y.DurationInMinutes);
        }
    }
}
