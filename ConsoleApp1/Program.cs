using Patterns;

namespace MazePrinter;

class Program
{
    private static List<MazeWriter> writers = null!;
    private static List<IMazeFormatter> formatters = null!;

    static void Main()
    {
        RegisterFormatters();
        var (length, width) = InputMazeParameters();
        var maze = RectangularMaze.GetMaze(length, width);
        var formatter = GetMazeFormatter();
        RegisterWriters(maze, formatter);
        var writer = GetMazeWriter();
        writer.Write();
        PrintLineWithColor("Запись завершена!", ConsoleColor.White);
        Console.ReadKey();
    }

    private static void RegisterFormatters()
    {
        formatters = [new DefaultMazeFormatter(), new WeirdFormatter()];
    }

    private static IMazeFormatter GetMazeFormatter()
    {
        PrintLineWithColor("Выберите способ вывода лабиринта: ", ConsoleColor.White);
        SetConsoleColor(ConsoleColor.Yellow);

        foreach (var format in formatters)
        {
            var newSymbols = format.symbols.Select(x => $"'{x}'");
            var symbols = string.Join(" | ", newSymbols);
            PrintLine($"{format.Name}: {symbols}");
        }

        return FindNamingElementByInput(formatters, "Такого способа вывода не существует");
    }

    private static MazeWriter GetMazeWriter()
    {
        PrintLineWithColor("Куда произвести запись?", ConsoleColor.White);
        var names = writers.Select(x => x.Name);
        SetConsoleColor(ConsoleColor.Yellow);
        PrintLine(string.Join(" | ", names));
        var writer = FindNamingElementByInput(writers, "Такого вида записи не существует");
        PrintLineWithColor($"Происходит запись на {writer.Name}...", ConsoleColor.White);
        return writer;
    }

    private static void RegisterWriters(IMaze maze, IMazeFormatter formatter)
    {
        writers = new()
        {
            new ConsoleMazeWriter(maze, Console.Out, formatter),
            new FileMazeWriter(maze, new StreamWriter("Labyrinth.txt"), formatter),
            new Writer(maze, Console.Out, formatter),
        };
    }

    private static T FindNamingElementByInput<T>(IEnumerable<T> collection, string errorMessage) where T : INaming
    {
        SetConsoleColor(ConsoleColor.Cyan);
        string input;
        T element;
        bool isError;
        do
        {
            input = Console.ReadLine()!;
            element = collection
                .FirstOrDefault(x => x.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            isError = element is null;
            if (isError) PrintError(errorMessage);
        } while (isError);

        return element!;
    }

    private static (int, int) InputMazeParameters()
    {
        PrintLineWithColor($"Введите размеры лабиринта: \nПример: 5 2", ConsoleColor.White);
        SetConsoleColor(ConsoleColor.Cyan);
        string[]? input;
        do
        {
            input = Console.ReadLine()?
                .Split();
        } while (CheckInput(input));

        var sizes = input!.Select(int.Parse)
            .ToArray();
        return sizes.ParseArrayToTuple();
    }

    private static bool CheckInput(string[]? input)
    {
        var isIncorrect = string.IsNullOrWhiteSpace(input?[0]) || input.Length != 2;
        if (isIncorrect)
            PrintError("Ошибка! Введите данные по примеру");
        return isIncorrect;
    }

    private static void SetConsoleColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }

    private static void PrintLineWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        var color = Console.ForegroundColor;
        SetConsoleColor(newColor);
        PrintLine(text);
        if (saveOldColor)
            SetConsoleColor(color);
    }

    private static void PrintLine(string text) => Console.WriteLine(text);
    private static void PrintError(string text) => PrintLineWithColor(text, ConsoleColor.Red);
}