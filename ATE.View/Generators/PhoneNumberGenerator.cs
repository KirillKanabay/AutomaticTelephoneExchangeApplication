using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Core.Specifications.Contracts;

namespace ATE.Generators
{
    public class PhoneNumberGenerator : IPhoneNumberGenerator
    {
        private readonly IRepository<Contract> _repo;

        public PhoneNumberGenerator(IRepository<Contract> repo)
        {
            _repo = repo;
        }
        
        public string Generate(string countryCode, string companyCode)
        {
            string phone = String.Empty;
            
            do
            {
                int userNumber = new Random().Next(0, 10000000);
                phone = $"+{countryCode}{companyCode}{userNumber.ToString()}";
            } while (_repo.GetEntityWithSpec(new ContractByPhoneNumberSpec(phone)) != null);

            return phone;
        }
        
    }
}