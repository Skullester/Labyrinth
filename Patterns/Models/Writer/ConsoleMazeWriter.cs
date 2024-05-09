using Patterns;

namespace MazePrinter;

public class ConsoleMazeWriter : MazeWriter
{
    private const double milliSeconds = 0.8;

    public ConsoleMazeWriter(IMaze maze, TextWriter writer, IMazeFormatter formatter) : base(maze, writer, formatter)
    {
    }

    public override string Name => "Консоль";

    protected override void Write(char sym)
    {
        writer.Write(sym);
        Thread.Sleep(TimeSpan.FromMilliseconds(milliSeconds));
    }
}