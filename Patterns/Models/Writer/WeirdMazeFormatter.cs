using Patterns;

namespace MazePrinter;

public class WeirdMazeFormatter : IMazeFormatter
{
    public string Name => "Нестандартный";
    public IReadOnlyList<char> symbols => symbolsArr.AsReadOnly();
    private readonly char[] symbolsArr = ['~', '%', '@', ')', '*'];

    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room => '~',
            ExternalWall => '%',
            InternalWall => '@',
            RoomWithSpikes => ')',
            Player => '*',
            _ => '?',
        };
    }
}