using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Enums;

namespace ATE.Entities.Calls
{
    public interface ICallPresenter
    {
        void Present(BaseBillingSystem billingSystem, Client client, CallSortType sort = CallSortType.Date);
    }
}