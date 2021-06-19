using System;
using ATE.Helpers;
using ATE.Views.Base;

namespace ATE.Views.Contracts
{
    class ContractMenuView : BaseMenuView
    {
        public ContractMenuView() : base("Управление контрактами")
        { }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            throw new System.NotImplementedException();
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