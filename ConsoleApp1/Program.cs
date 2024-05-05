using Patterns;

namespace MazePrinter;

class Program
{
    private static List<MazeWriter>? printers;

    static void Main()
    {
        var (length, width) = InputMazeParameters();
        var maze = RectangularMaze.GetMaze(length, width);
        RegisterPrinters(maze);
        var printer = GetMazeWriter(maze);
        printer.Print();
        PrintWithColor("Запись завершена!", ConsoleColor.White);
        Console.ReadKey();
    }

    private static void RegisterPrinters(IMaze maze)
    {
        printers = new()
        {
            new ConsoleMazeWriter(maze, Console.Out),
            new FileMazeWriter(maze, new StreamWriter("Labyrinth.txt")),
            //new Printer(maze, Console.Out),
        };
    }

    private static MazeWriter GetMazeWriter(IMaze maze)
    {
        PrintLine("Куда произвести запись?");
        var names = printers!.Select(x => x.GetDescription());
        PrintLine(string.Join(" | ", names));

        string input;
        MazeWriter? mazePrinter;
        var isError = false;
        do
        {
            input = Console.ReadLine()!;
            mazePrinter = printers
                .FirstOrDefault(x => x.GetDescription().StartsWith(input, StringComparison.OrdinalIgnoreCase));
            isError = mazePrinter is null;
            if (isError) PrintError("Такого вида записи не существует");
        } while (isError);

        PrintWithColor($"Происходит запись на {mazePrinter!.GetDescription()}...", ConsoleColor.White);
        return mazePrinter;
    }

    private static (int, int) InputMazeParameters()
    {
        var example = "\nПример: 5 2";
        PrintWithColor($"Введите длину и ширину лабиринта: {example}", ConsoleColor.Green);
        SetConsoleColor(ConsoleColor.Yellow);
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

    private static void PrintWithColor(string text, ConsoleColor newColor)
    {
        var color = Console.ForegroundColor;
        SetConsoleColor(newColor);
        PrintLine(text);
        SetConsoleColor(color);
    }

    private static void PrintLine(string text) => Console.WriteLine(text);
    private static void PrintError(string text) => PrintWithColor(text, ConsoleColor.Red);
}