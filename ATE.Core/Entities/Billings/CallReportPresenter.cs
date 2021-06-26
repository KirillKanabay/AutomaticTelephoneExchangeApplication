using System.Collections.Generic;

namespace ATE.Core.Entities.Billings
{
    public class CallReportPresenter
    {
        public IEnumerable<CallInformation> Calls { get; }

        public CallReportPresenter(IEnumerable<CallInformation> calls)
        {
            Calls = calls;
        }
        
    }
}