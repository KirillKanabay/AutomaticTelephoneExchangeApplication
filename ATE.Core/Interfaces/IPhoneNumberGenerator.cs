namespace ATE.Core.Interfaces
{
    public interface IPhoneNumberGenerator
    {
        string Generate(string countryCode, string companyCode);
    }
}