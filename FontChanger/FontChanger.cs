using System;
using System.Runtime.InteropServices;

namespace FontChanger
{
    /// <summary>
    /// Specifies constants for the different font families.
    /// </summary>
    public enum FontFamily {
        /// <summary>
        /// The Consolas font family (Windows 8+ default).
        /// </summary>
        Consolas,
        /// <summary>
        /// The Lucida Console font family.
        /// </summary>
        LucidaConsole,
        /// <summary>
        /// The Raster Fonts font family (Windows NT+ default).
        /// </summary>
        RasterFonts,
        /// <summary>
        /// The Courier New font family (Windows 8+ only).
        /// </summary>
        CourierNew,
        /// <summary>
        /// The Lucida Sans Typewrite font family (Windows 8+ only).
        /// </summary>
        LucidaSansTypewriter,
        /// <summary>
        /// The MS Gothic font family (Windows 8+ only).
        /// </summary>
        MSGothic,
        /// <summary>
        /// The NSimSun font family (Windows 8+ only).
        /// </summary>
        NSimSun,
        /// <summary>
        /// The SimSun-ExtB font family (Windows 8+ only).
        /// </summary>
        SimSunExtB
    }

    /// <summary>
    /// A class that manages the console font.
    /// </summary>
    public class ConsoleFont
    {   
        // a struct that stores our console font
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[32];
        }

        // a struct that stores the size of our console font
        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short x;
            internal short y;

            internal COORD(short x, short y)
            {
                this.x = x;
                this.y = y;
            }
        }

        // imports the change font function from the windows api
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        // imports the get handle function from the windows api
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int dwType);

        // imports the set font size function from the windows api
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetConsoleFont(IntPtr hOut, uint dwFontNum);

        // an array of our font families
        private static string[] fontFamilies = { "Consolas", "Lucida Console", "Raster Fonts", "CourierNew", "Lucida Sans Typewriter", "MS Gothic", "NSimSun", "SimSunExtB" };

        // gets the standard output handle
        private static IntPtr handle = GetStdHandle(-11);

        /// <summary>
        /// Changes the current console font and size (for Windows NT and up).
        /// </summary>
        /// <param name="font"></param>
        /// <param name="size"></param>
        public static unsafe void ChangeFont(FontFamily font, short size)
        {
            // if we are not on windows nt or later we return
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
            // sets console foreground color to default
            Console.ForegroundColor = ConsoleColor.Gray;
            // create out font details structure
            CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
            // sets the size of the structure
            newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
            // gets a pointer to the facename buffer
            IntPtr ptr = new IntPtr(newInfo.FaceName);
            // writes font family name to the buffer
            Marshal.Copy(fontFamilies[(int)font].ToCharArray(), 0, ptr, fontFamilies[(int)font].Length);
            // sets the font size
            newInfo.dwFontSize.y = size;
            // applies the font
            SetCurrentConsoleFontEx(handle, false, ref newInfo);
        }

        /// <summary>
        /// Changes the current console font, size and color (for Windows NT and up).
        /// </summary>
        /// <param name="font"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static unsafe void ChangeFont(FontFamily font, short size, ConsoleColor color)
        {
            // if we are not on windows nt or later we return
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
            // sets console foreground color
            Console.ForegroundColor = color;
            // create out font details structure
            CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
            // sets the size of the structure
            newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
            // gets a pointer to the facename buffer
            IntPtr ptr = new IntPtr(newInfo.FaceName);
            // writes font family name to the buffer
            Marshal.Copy(fontFamilies[(int)font].ToCharArray(), 0, ptr, fontFamilies[(int)font].Length);
            // sets the font size
            newInfo.dwFontSize.y = size;
            // applies the font
            SetCurrentConsoleFontEx(handle, false, ref newInfo);
        }

        /// <summary>
        /// Sets the current font size (for Windows NT and up).
        /// </summary>
        /// <param name="size"></param>
        public static void ChangeFontSize(uint size)
        {
            // if we are not on windows nt or later we return
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
            // sets the console font size
            SetConsoleFont(handle, size);
        }

        /// <summary>
        /// Writes message to standard output stream in the font and size given.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="size"></param>
        public static void WriteLineInFont(string text, FontFamily font, short size)
        {
            // changes the consoel font
            ChangeFont(font, size);
            // prints the text
            Console.WriteLine(text);
        }

        /// <summary>
        /// Writes message to standard output stream in the font, size and color given.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static void WriteLineInFont(string text, FontFamily font, short size, ConsoleColor color)
        {
            // changes the consoel font
            ChangeFont(font, size, color);
            // prints the text
            Console.WriteLine(text);
        }
    }
}
