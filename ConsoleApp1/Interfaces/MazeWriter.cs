using Patterns;

namespace MazePrinter;

public abstract class MazeWriter
{
    private readonly IMaze maze;
    protected readonly TextWriter writer;

    protected MazeWriter(IMaze maze, TextWriter writer)
    {
        this.maze = maze;
        this.writer = writer;
    }

    public void Print()
    {
        var counter = 1;
        var chars = maze.ParseToChar();
        foreach (var item in chars)
        {
            Print(item);
            if (counter++ % maze.Length == 0)
                Print('\n');
        }

        writer.Close();
    }

    protected abstract void Print(char sym);
    public abstract string GetDescription();
}