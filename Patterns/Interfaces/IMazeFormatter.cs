using Patterns;

namespace MazePrinter;

public interface IMazeFormatter : INaming
{
    IReadOnlyList<char> symbols { get; }
    char Format(IMazeElement element);
}