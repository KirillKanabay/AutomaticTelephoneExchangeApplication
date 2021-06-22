using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
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
            
            Console.Write("Введите название тарифа:");
            var name = Console.ReadLine();
            
            Console.Write("Введите цену за одну минуту звонка:");
            var pricePerMinuteCall = decimal.Parse(Console.ReadLine() ?? "0");
            
            return new Tariff(name, pricePerMinuteCall);
        }
    }
}