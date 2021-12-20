using System.Collections.Generic;
using ATE.Core.Entities.Billings;
using ATE.Core.Entities.Users;

namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingSystem
    {
        ICollection<CallInformation> Calls { get; }
        IBillingAccount Register(IContract contract);
    }
}