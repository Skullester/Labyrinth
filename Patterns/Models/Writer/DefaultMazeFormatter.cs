using Patterns;

namespace MazePrinter;

public class DefaultMazeFormatter : IMazeFormatter
{
    public char Format(IMazeElement element)
    {
        return element switch
        {
            Room => ' ',
            ExternalWall => '#',
            InternalWall => '*',
            RoomWithSpikes => 'x',
            _ => 'p',
        };
    }
}