namespace Patterns;

public interface IMaze : IEnumerable<IMazeElement>
{
    IMazeElement[,] Elements { get; }
    public int Height { get; }
    public int Width { get; }
}