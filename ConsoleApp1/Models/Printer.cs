using Patterns;

namespace MazePrinter;

class Printer : MazeWriter //заглушка
{
    public Printer(IMaze maze, TextWriter writer) : base(maze, writer)
    {
    }

    protected override void Print(char sym)
    {
        throw new NotImplementedException();
    }

    public override string GetDescription()
    {
        throw new NotImplementedException();
    }
}