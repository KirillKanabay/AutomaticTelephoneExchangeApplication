using ATE.Entities.Billings;

namespace ATE.Entities.Company.Creators
{
    public class MtsCompanyCreator : AbstractCompanyCreator
    {
        public override BaseCompany Create()
        {
            return new Company("МТС", new BillingSystem(), new PhoneNumberParameters("375", "29"));
        }
    }
}
