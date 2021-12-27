using ATE.Entities.Company;

namespace ATE.Interfaces
{
    public interface IPhoneNumberGenerator
    {
        string Generate(BaseCompany company);
    }
}