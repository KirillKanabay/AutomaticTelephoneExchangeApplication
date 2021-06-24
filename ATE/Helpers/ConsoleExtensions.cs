using System;

namespace ATE.Helpers
{
    public static class ConsoleEx
    {
        public static void WriteLineError(string error)
        {
            WriteLineWithColor(error, ConsoleColor.Red);
        }

        public static bool CheckContinue(string message)
        {
            char readedKey = ' ';
            do
            {
                Console.Write(message);
                readedKey = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
            } while (readedKey != 'y' && readedKey != 'n');
            
            return readedKey == 'y';
        }

        public static void WriteLineWithColor(string message, ConsoleColor foregroundColor)
        {
            var defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(message);

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
