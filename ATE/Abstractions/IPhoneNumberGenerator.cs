using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;

namespace ATE.Interfaces
{
    public interface IPhoneNumberGenerator
    {
        string Generate(BaseCompany company);
    }
}