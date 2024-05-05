using Patterns;

namespace MazePrinter;

public class ConsoleMazeWriter : MazeWriter
{
    public ConsoleMazeWriter(IMaze maze, TextWriter writer) : base(maze, writer)
    {
    }

    protected override void Print(char sym)
    {
        writer.Write(sym);
        Thread.Sleep(TimeSpan.FromMilliseconds(50));
    }

    public override string GetDescription() => "Консоль";
}