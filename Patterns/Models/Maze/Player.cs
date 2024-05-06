namespace Patterns;

public class Player : IMazeElement
{
    public int AppleCount { get; set; }

    public override string ToString()
    {
        return "P";
    }
}