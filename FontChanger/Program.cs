using System;

namespace FontChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            // changes the console font
            ConsoleFont.ChangeFont(FontFamily.LucidaConsole, 16);
            // prints a test sentence
            Console.WriteLine("Hello, World!");
            // waits to exit
            Console.ReadLine();
        }
    }
}
