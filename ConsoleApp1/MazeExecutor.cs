using MazePrinter;
using Patterns;

namespace ConsoleApp1;

class MazeExecutor
{
    private MazeWriter[] writers = null!;
    private IMazeFormatter[] formatters = null!;

    public void Start()
    {
        var (height, width) = GetMazeParameters();
        var maze = RectangularMaze.GetMaze(height, width);
        RegisterFormatters();
        PrintAllFormatters();
        var formatter = GetMazeFormatter();
        RegisterWriters(maze, formatter);
        PrintWriters();
        var writer = GetMazeWriter();
        ConsoleHelper.PrintLineWithColor($"Происходит запись на {writer.Name}...", ConsoleColor.White);
        writer.Write();
        ConsoleHelper.PrintLineWithColor("Запись завершена!", ConsoleColor.White);
        Console.ReadKey();
    }

    private void RegisterFormatters()
    {
        formatters = [new DefaultMazeFormatter(), new WeirdMazeFormatter()];
    }

    private IMazeFormatter GetMazeFormatter()
    {
        return FindNamingElementByInput(formatters, "Такого способа вывода не существует");
    }

    private void PrintAllFormatters()
    {
        ConsoleHelper.PrintLineWithColor("Выберите способ вывода лабиринта: ", ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Yellow);

        foreach (var format in formatters)
        {
            var newSymbols = format.Symbols.Select(x => $"'{x}'");
            var symbols = string.Join(" | ", newSymbols);
            ConsoleHelper.PrintLine($"{format.Name}: {symbols}");
        }
    }

    private MazeWriter GetMazeWriter()
    {
        return FindNamingElementByInput(writers, "Такого вида записи не существует");
    }

    private void PrintWriters()
    {
        ConsoleHelper.PrintLineWithColor("Куда произвести запись?", ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Yellow);
        var names = writers.Select(x => x.Name);
        ConsoleHelper.PrintLine(string.Join(" | ", names));
    }

    private void RegisterWriters(IMaze maze, IMazeFormatter formatter)
    {
        writers =
        [
            new ConsoleMazeWriter(maze, Console.Out, formatter),
            new FileMazeWriter(maze, new StreamWriter("Labyrinth.txt"), formatter),
            new Writer(maze, Console.Out, formatter)
        ];
    }

    private static T FindNamingElementByInput<T>(IEnumerable<T> collection, string errorMessage) where T : INaming
    {
        ConsoleHelper.SetConsoleColor(ConsoleColor.Cyan);
        T? element;
        bool isError;
        do
        {
            var input = Console.ReadLine()!;
            // ReSharper disable once PossibleMultipleEnumeration
            element = collection
                .FirstOrDefault(x => x.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            isError = element is null || string.IsNullOrWhiteSpace(input);
        } while (CheckError(isError, errorMessage));

        return element!;
    }

    private static bool CheckError(bool condition, string errorMessage)
    {
        if (condition) ConsoleHelper.PrintError(errorMessage);
        return condition;
    }

    private static (int, int) GetMazeParameters()
    {
        var input = InputMazeParameters();
        var sizes = input!.Select(int.Parse)
            .ToArray();
        return sizes.ParseArrayToTuple();
    }

    private static string[]? InputMazeParameters()
    {
        ConsoleHelper.PrintLineWithColor($"Введите высоту и ширину лабиринта: \nПример: 15 5", ConsoleColor.White);
        ConsoleHelper.SetConsoleColor(ConsoleColor.Cyan);
        string[]? input;
        do
        {
            input = Console.ReadLine()?
                .Split();
        } while (CheckInput(input));

        return input;
    }

    private static bool CheckInput(string[]? input)
    {
        var isIncorrect = string.IsNullOrWhiteSpace(input?[0]) || input.Length != 2;
        return CheckError(isIncorrect, "Ошибка! Введите данные по примеру");
    }
}