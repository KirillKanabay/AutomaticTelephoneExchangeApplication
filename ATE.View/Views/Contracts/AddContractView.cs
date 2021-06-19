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
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;

        public AddContractView(IRepository<Contract> contractRepo, IRepository<Client> clientRepo, IRepository<Company> companyRepo, IPhoneNumberGenerator phoneNumberGenerator)
        :base("Добавление контракта")
        {
            _contractRepo = contractRepo;
            _clientRepo = clientRepo;
            _companyRepo = companyRepo;
            _phoneNumberGenerator = phoneNumberGenerator;
        }

        public override void Show()
        {
            var client = SelectClient();
            var company = SelectCompany();
            var tariff = SelectTariff(company);
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
            foreach (var company in _companyRepo.List(spec))
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

        private Tariff SelectTariff(Company company)
        {
            Clear();
            foreach (var tariff in company.Tariffs)
            {
                Console.WriteLine(company);
            }

            Tariff gotTariff = null;
            while (true)
            {
                Console.Write("Введите Id тарифа:");
                try
                {
                    int idTariff = int.Parse(Console.ReadLine() ?? "-1");
                    gotTariff = company.Tariffs.Find(t => t.Id == idTariff);
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