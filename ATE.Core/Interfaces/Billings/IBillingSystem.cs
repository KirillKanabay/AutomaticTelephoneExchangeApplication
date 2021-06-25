using ATE.Core.Entities;
using ATE.Core.Entities.Users;

namespace ATE.Core.Interfaces.Billings
{
    public interface IBillingSystem
    {
        IBillingAccount Register(Company company);
        IBillingAccount Register(User user);
    }
}