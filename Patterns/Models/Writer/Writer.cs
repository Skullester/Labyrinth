using Patterns;

namespace MazePrinter;

public class Writer : MazeWriter //заглушка
{
    public Writer(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    protected override void Print(char sym)
    {
        Console.WriteLine("print");
    }

    public override string GetDescription()
    {
        return "lexa";
    }
}