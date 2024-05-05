namespace Patterns;

public interface IMaze : IEnumerable<IMazeElement>
{
    IMazeElement[,] Elements { get; }
    public int Length { get; }
    public int Width { get; }
}