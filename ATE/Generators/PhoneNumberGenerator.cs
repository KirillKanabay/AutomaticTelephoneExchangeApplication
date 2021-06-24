using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Core.Specifications.Contracts;

namespace ATE.Generators
{
    public class PhoneNumberGenerator : IPhoneNumberGenerator
    {
        public string Generate(Company company)
        {
            int userNumber = new Random().Next(1, 10000000);
            string phone = $"+{company.CountryCode}{company.CompanyCode}{userNumber:D7}";

            return phone;
        }
        
    }
}