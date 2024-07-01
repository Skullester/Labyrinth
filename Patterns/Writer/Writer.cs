using Patterns;

namespace MazePrinter;

public class Writer : MazeWriter //заглушка
{
    public Writer(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    public override string Name => "Заглушка";

    protected override void Write(char sym)
    {
        Console.WriteLine("print");
    }
}