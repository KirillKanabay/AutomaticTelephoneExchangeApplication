using System.Collections.Generic;
using System.Linq;
using ATE.Core.Enums;
using ATE.Core.Interfaces.Billings;

namespace ATE.Core.Entities.Billings
{
    public class CallReporter
    {
        public IEnumerable<CallInformation> Calls { get; }

        public CallReporter(IBillingSystem bs, IBillingAccount acc)
        {
            Calls = bs.Calls.Where(c => c.ClientPhoneNumber == acc.Contract.PhoneNumber);
        }

        public IEnumerable<CallInformation> Render(CallSort callSort) => callSort switch
        {
            CallSort.Date => Calls.OrderBy(c => c.CallDate),
            CallSort.Price => Calls.OrderBy(c => c.Price),
            CallSort.Subscriber => Calls.OrderBy(c => c.DestinationPhoneNumber)
        };
    }
}