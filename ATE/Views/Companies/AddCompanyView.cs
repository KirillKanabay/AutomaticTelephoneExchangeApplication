using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Companies
{
    class AddCompanyView : BaseView
    {
        private readonly IRepository<Core.Entities.Company> _repo;
        public AddCompanyView(IRepository<Core.Entities.Company> repo) : base("Добавление компании")
        {
            _repo = repo;
        }

        public override void Show()
        {
            var company = ReadCompany();
            if(company != null)
            {
                _repo.Add(company);
                ConsoleEx.WriteLineWithColor("Компания добавлена. Нажмите любую кнопку...", ConsoleColor.Green);
            }
            else
            {
                ConsoleEx.WriteLineError("Ошибка добавления компании. Нажмите любую кнопку...");
            }
            Console.ReadKey();
        }

        private Core.Entities.Company ReadCompany()
        {
            Console.Write("Введите название компании:");
            var name = Console.ReadLine();

            Console.Write("Введите код страны компании:");
            var countryCode = Console.ReadLine();
            
            Console.Write("Введите код компании:");
            var companyCode = Console.ReadLine();
            
            return new Company(name, countryCode, companyCode);
        }
    }
}
