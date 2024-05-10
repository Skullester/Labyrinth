namespace Patterns;

public interface IMazeElement
{
    bool IsVisited { get; set; }
    int Distance { get; set; }
}