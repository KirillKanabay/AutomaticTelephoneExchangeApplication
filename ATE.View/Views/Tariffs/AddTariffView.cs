using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Core.Specifications.Companies;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Tariffs
{
    public class AddTariffView : BaseView
    {
        private readonly IRepository<Tariff> _tariffRepo;
        private readonly IRepository<Company> _companyRepo;

        public AddTariffView(IRepository<Tariff> tariffRepo, IRepository<Company> companyRepo) : base("Добавление тарифа")
        {
            _tariffRepo = tariffRepo;
            _companyRepo = companyRepo;
        }

        public override void Show()
        {
            var company = SelectCompany();
            var tariff = ReadTariff(company);
            _tariffRepo.Add(tariff);
        }

        private Company SelectCompany()
        {
            Clear();
            foreach (var company in _companyRepo.ListAll())
            {
                Console.WriteLine(company);
            }

            Company gotCompany = null;
            while (true)
            {
                Console.Write("Введите Id компании:");
                try
                {
                    int idCompany= int.Parse(Console.ReadLine() ?? "-1");
                    gotCompany = _companyRepo.GetById(idCompany);
                }
                catch
                {
                    ConsoleEx.WriteLineError("Ошибка ввода, попробуйте еще раз...");
                }

                if (gotCompany == null)
                {
                    ConsoleEx.WriteLineError("Такой компании не существует. Попробуйте еще раз...");
                }
                break;
            }

            return gotCompany;
        }
        
        private Tariff ReadTariff(Company company)
        {
            Clear();
            var tariff = new Tariff();
            
            Console.Write("Введите название тарифа:");
            tariff.Name = Console.ReadLine();
            
            Console.Write("Введите цену за одну минуту звонка:");
            tariff.PricePerCall = decimal.Parse(Console.ReadLine() ?? "0");

            tariff.CompanyId = company.Id;

            return tariff;
        }
    }
}