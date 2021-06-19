using System;

namespace ATE.Helpers
{
    public delegate void KeyHandler(object source, KeyEventArgs e);

    public class KeyEventArgs : EventArgs
    {
        public ConsoleKeyInfo ConsoleKeyInfo;
    }

    public class KeyEvent
    {
        public event KeyHandler KeyPressEvent;
        public void OnKeyPress(ConsoleKeyInfo consoleKeyInfo)
        {
            KeyPressEvent?.Invoke(this, new KeyEventArgs() {ConsoleKeyInfo = consoleKeyInfo});
        }
    }
}