using System;
using ATE.Helpers;

namespace ATE.Views.Base
{
    abstract class BaseMenuView : BaseView 
    {
        protected readonly KeyEvent KeyEvent;
        protected BaseMenuView(KeyEvent keyEvent)
        {
            KeyEvent = keyEvent;
            KeyEvent.KeyPressEvent += OnKeyPress;
        }

        protected abstract void OnKeyPress(object sender, KeyEventArgs e);
        protected abstract void ShowHelp();
        public override void Show()
        {
            Console.Title = Title;
            Clear();
            ShowHelp();

            ConsoleKeyInfo cki;
            do
            {
                Console.Write("Нажмите клавишу <F1 - справка>:");
                cki = Console.ReadKey(false);
                Console.WriteLine();
                KeyEvent.OnKeyPress(cki);
            } while (cki.Key != ConsoleKey.F4);
        }
    }
}
