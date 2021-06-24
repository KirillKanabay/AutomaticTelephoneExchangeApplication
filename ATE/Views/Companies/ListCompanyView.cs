using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Companies
{
    class ListCompanyView : BaseView
    {
        private readonly IRepository<Company> _repo;
        public ListCompanyView(IRepository<Company> repo) : base("Список компаний")
        {
            _repo = repo;
        }

        public override void Show()
        {
            Clear();
            foreach(var company in _repo.ListAll())
            {
                Console.WriteLine(company);
            }

            ConsoleEx.WriteLineWithColor("Нажмите на кнопку, чтобы продолжить...", ConsoleColor.Green);
            Console.ReadKey();
        }
    }
}
