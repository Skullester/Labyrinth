using Patterns;

namespace MazePrinter;

public class FileMazeWriter : MazeWriter
{
    public FileMazeWriter(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    public override string Name => "Файл";

    protected override void Write(char sym)
    {
        writer.Write(sym);
        writer.Flush();
    }
}