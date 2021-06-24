using System;
using ATE.Helpers;

namespace ATE.Views.Base
{
    public abstract class BaseMenuView : BaseView 
    {
        protected readonly KeyEvent KeyEvent;
        protected BaseMenuView(string title) : base(title)
        {
            KeyEvent = new KeyEvent();
            KeyEvent.KeyPressEvent += OnKeyPress;
        }

        protected abstract void OnKeyPress(object sender, KeyEventArgs e);
        protected abstract void ShowHelp();
        public override void Show()
        {
            Console.Title = Title;
            
            ConsoleKeyInfo cki;
            do
            {
                Clear();
                ShowHelp();
                
                Console.Write("Нажмите клавишу:");
                
                cki = Console.ReadKey(false);
                Console.WriteLine();
                KeyEvent.OnKeyPress(cki);
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
