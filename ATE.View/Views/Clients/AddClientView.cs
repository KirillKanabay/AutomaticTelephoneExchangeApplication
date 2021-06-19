using System;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Clients
{
    class AddClientView : BaseView
    {
        private readonly IRepository<Client> _repo;

        public AddClientView(IRepository<Client> repo) : base("Добавить клиента")
        {
            _repo = repo;
        }

        public override void Show()
        {
            var client = ReadClient();
            if (client != null)
            {
                _repo.Add(client);
                ConsoleEx.WriteLineWithColor("Клиент добавлен. Нажмите любую кнопку...", ConsoleColor.Green);
            }
            else
            {
                ConsoleEx.WriteLineError("Ошибка добавления клиента. Нажмите любую кнопку...");
            }
            Console.ReadKey();
        }

        private Client ReadClient()
        {
            Console.Write("Введите имя клиента:");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию клиента:");
            string secondName = Console.ReadLine();

            return new Client {FirstName = name, SecondName = secondName};
        }
    }
}