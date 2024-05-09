using Patterns;

namespace MazePrinter;

public abstract class MazeWriter : INaming
{
    private readonly IMaze maze;
    public abstract string Name { get; }
    protected readonly TextWriter writer;
    private readonly IMazeFormatter mazeFormatter;


    protected MazeWriter(IMaze maze, TextWriter writer, IMazeFormatter mazeFormatter)
    {
        this.maze = maze;
        this.writer = writer;
        this.mazeFormatter = mazeFormatter;
    }

    public void Write()
    {
        var counter = 1;
        var chars = maze.ParseToChar(mazeFormatter);
        foreach (var item in chars)
        {
            Write(item);
            if (counter++ % maze.Height == 0)
                Write('\n');
        }

        writer.Close();
    }

    protected abstract void Write(char sym);
}