using System;

namespace ATE.Views.Base
{
    public abstract class BaseView
    {
        public string Title { get; protected set; }
        public string TitleWindow { get; protected set; }

        public BaseView(string title)
        {
            Title = title;
            TitleWindow = $"Automatic Telephone Exchange - {title}";
            Clear();
        }

        /// <summary>
        /// Показ представления
        /// </summary>
        public abstract void Show();

        protected virtual void Clear()
        {
            Console.Clear();
            Console.Title = TitleWindow;
            Console.WriteLine($"\t{Title}");
        }

        /// <summary>
        /// Выход из программы
        /// </summary>
        protected virtual void Exit()
        {
            Environment.Exit(0);
        }
    }
}
