using Patterns;

namespace MazePrinter;

public interface IMazeFormatter
{
    char Format(IMazeElement element);
}