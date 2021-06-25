using System;
using System.Linq;
using ATE.Core.Entities;
using ATE.Core.Interfaces;

namespace ATE.Core.Generators
{
    public class PhoneNumberGenerator : IPhoneNumberGenerator
    {
        public string Generate(ICompany company)
        {
            string phoneNumber;
            
            do
            {
                int userNumber = new Random().Next(1, 10000000);
                phoneNumber = $"+{company.NumberParams.CountryCode}{company.NumberParams.CompanyCode}{userNumber:D7}";
                if (company.Contracts.Any(c => c.PhoneNumber == phoneNumber))
                {
                    phoneNumber = String.Empty;
                }
            } while (phoneNumber == String.Empty);
            
            return phoneNumber;
        }
        
    }
}