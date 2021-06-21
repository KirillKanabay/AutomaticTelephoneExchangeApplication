using System;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Contracts
{
    class ContractMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;

        public ContractMenuView(ViewContainer viewContainer) : base("Управление контрактами")
        {
            _viewContainer = viewContainer;
        }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                case ConsoleKey.F4:
                    Exit();
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.D1:
                    _viewContainer.Resolve<AddContractView>().Show();
                    break;
                default:
                    ConsoleEx.WriteLineError("Такой команды не существует! Нажмите любую кнопку...");
                    Console.ReadKey();
                    break;
            }
        }

        protected override void ShowHelp()
        {
            Clear();
            Console.WriteLine($"<F4> - Выход из программы{Environment.NewLine}" +
                              $"<ESC> - Назад{Environment.NewLine}" +
                              $"<1> - Добавление контракта{Environment.NewLine}" +
                              $"<2> - Просмотр контрактов{Environment.NewLine}");
        }
    }
}