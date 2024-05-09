namespace Patterns;

public class Player : IMazeElement
{
    public bool IsVisited { get; set; }

    public override string ToString()
    {
        return "P";
    }
}