using Patterns;

namespace MazePrinter;

public class WeirdFormatter : IMazeFormatter
{
    public string Name => "Нестандартный";
    public IReadOnlyList<char> symbols => symbolsArr.AsReadOnly();
    private readonly char[] symbolsArr = ['$', '@', '!', ')', '('];

    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room => '$',
            ExternalWall => '@',
            InternalWall => '!',
            RoomWithSpikes => ')',
            Player => '(',
            _ => '?',
        };
    }
}