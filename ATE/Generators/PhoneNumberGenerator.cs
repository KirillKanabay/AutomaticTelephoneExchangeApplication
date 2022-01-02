using System;
using ATE.Abstractions.Domain.Company;
using ATE.Domain.Company;
using ATE.Interfaces;

namespace ATE.Generators
{
    public class PhoneNumberGenerator : IPhoneNumberGenerator
    {
        public string Generate(BaseCompany company)
        {
            var options = company.PhoneNumberOptions;
            
            string phoneNumber;
            
            do
            {
                int userNumber = new Random().Next(1, 10000000);
                phoneNumber = $"+{options.CountryCode}{options.CompanyCode}{userNumber:D7}";
                
                if (company.PhoneNumberExists(phoneNumber))
                {
                    phoneNumber = String.Empty;
                }

            } while (phoneNumber == String.Empty);
            
            return phoneNumber;
        }
        
    }
}