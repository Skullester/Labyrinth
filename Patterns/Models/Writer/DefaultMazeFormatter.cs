using Patterns;

namespace MazePrinter;

public class DefaultMazeFormatter : IMazeFormatter
{
    public string Name => "Стандартный";
    public IReadOnlyList<char> symbols => symbolsArr.AsReadOnly();
    private readonly char[] symbolsArr = [' ', '#', '*', 'x', 'p'];

    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room => ' ',
            ExternalWall => '#',
            InternalWall => '*',
            RoomWithSpikes => 'x',
            Player => 'p',
            _ => '?',
        };
    }
}