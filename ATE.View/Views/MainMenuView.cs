using System;
using ATE.Helpers;
using ATE.Views.Base;
using ATE.Views.Companies;

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
                case ConsoleKey.F4:
                    Exit();
                    break;
                case ConsoleKey.D1:
                    _viewContainer.Resolve<CompanyMenuView>().Show();
                    Clear();
                    break;
                default:
                    ConsoleEx.WriteLineError("Такой команды не существует!");
                    break;
            }
        }

        protected override void ShowHelp()
        {
            Console.WriteLine($"<F4> - Выход из программы{Environment.NewLine}" +
                              $"<1> - Управление компаниями{Environment.NewLine}" +
                              $"<2> - Управление клиентами{Environment.NewLine}");
        }
    }
}