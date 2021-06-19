using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Clients
{
    class ListClientView:BaseView
    {
        private readonly IRepository<Client> _repo;

        public ListClientView(IRepository<Client> repo) : base("Список клиентов:")
        {
            _repo = repo;
        }

        public override void Show()
        {
            Clear();
            
            var clients = _repo.ListAll();
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
            
            ConsoleEx.WriteLineWithColor("Нажмите на кнопку, чтобы продолжить...", ConsoleColor.Green);
            Console.ReadKey();
        }
    }
}