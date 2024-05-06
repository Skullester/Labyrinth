using Patterns;

namespace MazePrinter;

public class ConsoleMazeWriter : MazeWriter
{
    private const double milliSeconds = 0;

    public ConsoleMazeWriter(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    protected override void Print(char sym)
    {
        writer.Write(sym);
        Thread.Sleep(TimeSpan.FromMilliseconds(milliSeconds));
    }

    public override string GetDescription() => "Консоль";
}