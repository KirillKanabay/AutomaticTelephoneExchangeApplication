using ATE.Core.Entities;

namespace ATE.Core.Interfaces
{
    public interface IPhoneNumberGenerator
    {
        string Generate(Company company);
    }
}