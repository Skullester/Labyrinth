using Patterns;

namespace MazePrinter;

public interface IMazeFormatter : INaming
{
    IReadOnlyList<char> Symbols { get; }
    char Format(IMazeElement element);
}