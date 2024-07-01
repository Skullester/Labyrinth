namespace Patterns;

public class Player : IMazeElement
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
}