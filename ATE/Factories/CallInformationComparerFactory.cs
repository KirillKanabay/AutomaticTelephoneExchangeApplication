using System;
using System.Collections.Generic;
using ATE.Abstractions.Factories;
using ATE.Comparers.Calls;
using ATE.Entities.Calls;
using ATE.Enums;

namespace ATE.Factories
{
    public class CallInformationComparerFactory : AbstractCallInformationComparerFactory
    {
        public override IComparer<CallInformation> Create(CallSortType sort) => sort switch{
            CallSortType.Date     => new CallInformationDateComparer(),
            CallSortType.Duration => new CallInformationDurationComparer(),
            CallSortType.Price    => new CallInformationPriceComparer(),
            _ => throw new ArgumentException("Unsupported call sort type"),
        };
}
}
