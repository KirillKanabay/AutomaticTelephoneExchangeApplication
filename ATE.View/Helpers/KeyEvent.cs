using System;

namespace ATE.Helpers
{
    internal delegate void KeyHandler(object source, KeyEventArgs e);

    internal class KeyEventArgs : EventArgs
    {
        public ConsoleKeyInfo ConsoleKeyInfo;
    }

    internal class KeyEvent
    {
        public event KeyHandler KeyPressEvent;
        public void OnKeyPress(ConsoleKeyInfo consoleKeyInfo)
        {
            KeyPressEvent?.Invoke(this, new KeyEventArgs() {ConsoleKeyInfo = consoleKeyInfo});
        }
    }
}