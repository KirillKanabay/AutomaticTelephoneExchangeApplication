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
            var tariff = ReadTariff();
            _tariffRepo.Add(tariff);
        }
        
        private Tariff ReadTariff()
        {
            Clear();
            var tariff = new Tariff();
            
            Console.Write("Введите название тарифа:");
            tariff.Name = Console.ReadLine();
            
            Console.Write("Введите цену за одну минуту звонка:");
            tariff.PricePerMinuteCall = decimal.Parse(Console.ReadLine() ?? "0");
            
            return tariff;
        }
    }
}