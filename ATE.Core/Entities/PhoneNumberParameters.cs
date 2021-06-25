namespace ATE.Core.Entities
{
    public struct PhoneNumberParameters
    {
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }

        public PhoneNumberParameters(string countryCode, string companyCode)
        {
            CountryCode = countryCode;
            CompanyCode = companyCode;
        }
    }
}