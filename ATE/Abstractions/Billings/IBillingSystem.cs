using System.Collections.Generic;
using ATE.Entities.Billings;
using ATE.Entities.Calls;

namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingSystem
    {
        ICollection<CallInformation> Calls { get; }
        IBillingAccount Register(IContract contract);
    }
}