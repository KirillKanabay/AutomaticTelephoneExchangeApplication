using System;
using ATE.Helpers;
using ATE.Views.Base;
using ATE.Views.Company;

namespace ATE.Views
{
    internal class MainMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;
        public MainMenuView(KeyEvent keyEvent, ViewContainer viewContainer) : base(keyEvent)
        {
            _viewContainer = viewContainer;
        }

        public override string Title { get; protected set; } = "Automatic Telephone Exchange - Главное окно";
        public override string Header { get; protected set; } = "Главное окно";
        
        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                case ConsoleKey.F1:
                    ShowHelp();
                    break;
                case ConsoleKey.F4:
                    Exit();
                    break;
                case ConsoleKey.D1:
                    _viewContainer.Resolve<CompanyMenuView>().Show();
                    break;
                default:
                    ConsoleEx.WriteLineError("Такой команды не существует!");
                    break;
            }
        }

        protected override void ShowHelp()
        {
            Console.WriteLine($"<F1> - Справка{Environment.NewLine}" +
                              $"<F4> - Выход из программы{Environment.NewLine}" +
                              $"<1> - Управление компаниями{Environment.NewLine}" +
                              $"<2> - Управление клиентами{Environment.NewLine}");
        }
    }
}