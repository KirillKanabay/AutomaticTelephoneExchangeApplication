namespace ATE.Entities
{
    public class PhoneNumberOptions
    {
        public readonly string CountryCode;
        public readonly string CompanyCode; 

        public PhoneNumberOptions(string countryCode, string companyCode)
        {
            CountryCode = countryCode;
            CompanyCode = companyCode;
        }
    }
}