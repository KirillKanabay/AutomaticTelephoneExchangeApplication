using System;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Companies
{
    internal class CompanyMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;
        public CompanyMenuView(ViewContainer viewContainer) : base("Управление компаниями")
        {
            _viewContainer = viewContainer;
        }

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
                    _viewContainer.Resolve<AddCompanyView>().Show();
                    break;
                case ConsoleKey.D2:
                    _viewContainer.Resolve<ListCompanyView>().Show();
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    ConsoleEx.WriteLineError("Такой команды не существует! Нажмите любую кнопку...");
                    Console.ReadKey();
                    break;
            }
            Clear();
        }

        protected override void ShowHelp()
        {
            Clear();
            Console.WriteLine($"<Esc> - Назад{Environment.NewLine}" +
                              $"<F4> - Выход из программы{Environment.NewLine}" +
                              $"<1> - Добавить компанию{Environment.NewLine}" +
                              $"<2> - Посмотреть список компаний{Environment.NewLine}");
        }
    }
}