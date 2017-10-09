# FontChanger
A simple wrapper for changing the Windows console font and size.

# Example Usage
```C#
public void FontExample()
{
    // changes the console font
    ConsoleFont.ChangeFont(FontFamily.LucidaConsole, 16);
    // prints a test sentence
    Console.WriteLine("Hello, World!");
    // waits to exit
    Console.ReadLine();
}
```
