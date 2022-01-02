using ATE.Abstractions.Domain.Billings;
using ATE.Domain.Company;
using ATE.Domain.Users;
using ATE.Enums;

namespace ATE.Abstractions.Domain.Calls
{
    public interface ICallPresenter
    {
        void Present(BaseBillingSystem billingSystem, Client client, CallSortType sort = CallSortType.Date);
    }
}