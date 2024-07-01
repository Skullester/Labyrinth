namespace ConsoleApp1;

public static class ConsoleHelper
{
    public static void SetConsoleColor(ConsoleColor color) => Console.ForegroundColor = color;

    public static void PrintLineWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        var color = Console.ForegroundColor;
        SetConsoleColor(newColor);
        PrintLine(text);
        if (saveOldColor)
            SetConsoleColor(color);
    }

    public static void PrintLine(string text) => Console.WriteLine(text);
    public static void PrintError(string text) => PrintLineWithColor(text, ConsoleColor.Red);
}