namespace ATE.Core.Entities
{
    public readonly struct PhoneNumberParameters
    {
        public readonly string CountryCode;
        public readonly string CompanyCode; 

        public PhoneNumberParameters(string countryCode, string companyCode)
        {
            CountryCode = countryCode;
            CompanyCode = companyCode;
        }
    }
}