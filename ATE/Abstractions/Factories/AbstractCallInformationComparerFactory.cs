using System.Collections.Generic;
using ATE.Domain.Calls;
using ATE.Enums;

namespace ATE.Abstractions.Factories
{
    public abstract class AbstractCallInformationComparerFactory
    {
        public abstract IComparer<CallInformation> Create(CallSortType sort);
    }
}
