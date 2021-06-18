using System;

namespace ATE.Views.Base
{
    internal abstract class BaseView
    {
        public abstract string Title { get; protected set; }
        public abstract string Header { get; protected set; }
        
        /// <summary>
        /// Показ представления
        /// </summary>
        public abstract void Show();

        protected virtual void Clear()
        {
            Console.Clear();
            Console.WriteLine($"\t{Header}");
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
