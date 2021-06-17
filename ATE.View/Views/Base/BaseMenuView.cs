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
    }
}
