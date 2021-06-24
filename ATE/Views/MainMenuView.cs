using System;
using ATE.Core.Entities;
using ATE.Helpers;
using ATE.Views.Base;
using ATE.Views.Clients;
using ATE.Views.Companies;
using ATE.Views.Contracts;
using ATE.Views.Tariffs;

namespace ATE.Views
{
    internal class MainMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;
        public MainMenuView(ViewContainer viewContainer) : base("Главное окно")
        {
            _viewContainer = viewContainer;
        }
       
        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                case ConsoleKey.F1:
                    Clear();
                    ShowHelp();
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.F4:
                    Exit();
                    break;
                case ConsoleKey.D1:
                    _viewContainer.Resolve<CompanyMenuView>().Show();
                    Clear();
                    break;
                case ConsoleKey.D2:
                    _viewContainer.Resolve<ClientMenuView>().Show();
                    Clear();
                    break;
                case ConsoleKey.D3:
                    _viewContainer.Resolve<TariffMenuView>().Show();
                    Clear();
                    break;
                case ConsoleKey.D4:
                    _viewContainer.Resolve<ContractMenuView>().Show();
                    Clear();
                    break;
                default:
                    ConsoleEx.WriteLineError("Такой команды не существует! Нажмите любую кнопку...");
                    Console.ReadKey();
                    break;
            }
        }

        protected override void ShowHelp()
        {
            Console.WriteLine($"<F4> - Выход из программы{Environment.NewLine}" +
                              $"<1> - Управление компаниями{Environment.NewLine}" +
                              $"<2> - Управление клиентами{Environment.NewLine}" +
                              $"<3> - Управление тарифами{Environment.NewLine}" +
                              $"<4> - Управление контрактами{Environment.NewLine}");
        }
    }
}