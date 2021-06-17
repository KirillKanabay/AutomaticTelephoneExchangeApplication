using System;

namespace ATE.Views.Base
{
    internal abstract class BaseView
    {
        /// <summary>
        /// Показ представления
        /// </summary>
        public abstract void Show();
        
        /// <summary>
        /// Выход из программы
        /// </summary>
        protected virtual void Exit()
        {
            Environment.Exit(0);
        }
    }
}
