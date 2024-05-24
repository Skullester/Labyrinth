using Patterns;

namespace MazePrinter;

public class DefaultMazeFormatter : IMazeFormatter
{
    public string Name => "Стандартный";
    public IReadOnlyList<char> Symbols => symbolsArr.AsReadOnly();
    private readonly char[] symbolsArr = [' ', '#', '*', 'Q', 'P'];

    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room or ExitRoom => ' ',
            ExternalWall => '#',
            InternalWall => '*',
            // ExitRoom => 'Q',
            Player => 'P',
            _ => '?',
        };
    }
}