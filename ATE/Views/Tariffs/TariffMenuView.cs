using System;
using ATE.Helpers;
using ATE.Views.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ATE.Views.Tariffs
{
    public class TariffMenuView : BaseMenuView
    {
        private readonly ViewContainer _viewContainer;
        public TariffMenuView(ViewContainer viewContainer) : base("Управление тарифами")
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
                    _viewContainer.Resolve<AddTariffView>().Show();
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
                              $"<Esc> - Назад{Environment.NewLine}" +
                              $"<1> - Добавить тариф");
        }
    }
}