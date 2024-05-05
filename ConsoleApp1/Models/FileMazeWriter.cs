using Patterns;

namespace MazePrinter;

public class FileMazeWriter : MazeWriter
{
    public FileMazeWriter(IMaze maze, TextWriter writer) : base(maze, writer)
    {
    }

    protected override void Print(char sym)
    {
        writer.Write(sym);
        writer.Flush();
    }

    public override string GetDescription() => "Файл";
}