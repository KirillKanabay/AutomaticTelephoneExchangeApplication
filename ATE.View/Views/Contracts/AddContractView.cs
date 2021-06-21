using System;
using System.Linq;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Core.Specifications.Companies;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Contracts
{
    class AddContractView : BaseView
    {
        private readonly IRepository<Contract> _contractRepo;
        private readonly IRepository<Client> _clientRepo;
        private readonly IRepository<Company> _companyRepo;
        private readonly IRepository<Tariff> _tariffRepo;
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;

        public AddContractView(IRepository<Contract> contractRepo, IRepository<Client> clientRepo, IRepository<Company> companyRepo, 
            IPhoneNumberGenerator phoneNumberGenerator, IRepository<Tariff> tariffRepo)
        :base("Добавление контракта")
        {
            _contractRepo = contractRepo;
            _clientRepo = clientRepo;
            _companyRepo = companyRepo;
            _tariffRepo = tariffRepo;
            _phoneNumberGenerator = phoneNumberGenerator;
        }

        public override void Show()
        {
            var client = SelectClient();
            var company = SelectCompany();
            var tariff = SelectTariff();
            
            Clear();
            var number = _phoneNumberGenerator.Generate(company.CountryCode, company.CompanyCode);
            Console.WriteLine($"Сгенерированный номер телефона: {number}");
            
            _contractRepo.Add(new Contract(){ClientId = client.Id, CompanyId = company.Id, PhoneNumber = number, TariffId = tariff.Id});
            
            ConsoleEx.WriteLineWithColor("Контракт успешно добавлен. Нажмите любую кнопку...", ConsoleColor.Green);
            Console.ReadKey();
        }

        private Client SelectClient()
        {
            Clear();
            
            foreach (var client in _clientRepo.ListAll())
            {
                Console.WriteLine(client);
            }

            Client gotClient = null;
            while (true)
            {
                Console.Write("Введите Id клиента:");
                try
                {
                    int idClient = int.Parse(Console.ReadLine() ?? "-1");
                    gotClient = _clientRepo.GetById(idClient);
                }
                catch
                {
                    ConsoleEx.WriteLineError("Ошибка ввода, попробуйте еще раз...");
                }

                if (gotClient == null)
                {
                    ConsoleEx.WriteLineError("Такого клиента не существует. Попробуйте еще раз...");
                }
                break;
            }

            return gotClient;
        }

        private Company SelectCompany()
        {
            Clear();
            var spec = new CompanyWithTariffsSpec();
            var companies = _companyRepo.List(spec);
            foreach (var company in companies)
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
                    gotCompany = companies.FirstOrDefault(c => c.Id == idCompany);
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

        private Tariff SelectTariff()
        {
            Clear();
            foreach (var tariff in _tariffRepo.ListAll())
            {
                Console.WriteLine(tariff);
            }

            Tariff gotTariff = null;
            while (true)
            {
                Console.Write("Введите Id тарифа:");
                try
                {
                    int idTariff = int.Parse(Console.ReadLine() ?? "-1");
                    gotTariff = _tariffRepo.GetById(idTariff);
                }
                catch
                {
                    ConsoleEx.WriteLineError("Ошибка ввода, попробуйте еще раз...");
                }

                if (gotTariff == null)
                {
                    ConsoleEx.WriteLineError("Такого тарифа не существует. Попробуйте еще раз...");
                }
                break;
            }

            return gotTariff;
        }
    }
}