# FontChanger
A simple wrapper for changing the Windows console font and size.

# Example Usage
Changing the current console font and size.
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
Changing the current console font size.
```C#
public void FontSizeExample()
{
    // changes the console font size
    ConsoleFont.ChangeFontSize(5);
    // prints a test sentence
    Console.WriteLine("Hello, World!");
    // waits to exit
    Console.ReadLine();
}
```
