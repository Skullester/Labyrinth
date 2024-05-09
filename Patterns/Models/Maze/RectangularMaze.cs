using System.Collections;
using System.Drawing;

namespace Patterns;

public class RectangularMaze : IMaze
{
    public int Height { get; }
    public int Width { get; }
    private static RectangularMaze? maze;

    public IMazeElement[,] Elements { get; private set; } = null!;

    private RectangularMaze(int height, int width)
    {
        Height = height;
        Width = width;
        InitializeExternalWalls();
        InitializeObjects();
    }

    private RoomCreator[] fabric =
    [
        new DefaultRoomCreator(),
        // new RoomSpikesCreator()
    ];

    private void InitializeExternalWalls()
    {
        Elements = new IMazeElement[Height, Width];
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (i == 0 || i == Height - 1 || j == 0 || j == Width - 1)
                    this[i, j] = new ExternalWall();
                else this[i, j] = new InternalWall(100);
            }
        }
    }

    private void InitializeObjects()
    {
        var rand = new Random();
        var startX = rand.Next(1, Width);
        var startY = rand.Next(1, Height);
        var startPoint = new Point(startX, startY);
        var currentPoint = startPoint;
        var stack = new Stack<Point>();
        do
        {
            var neighbours = new List<Point>();
            var x = currentPoint.X;
            var y = currentPoint.Y;
            this[x, y] = fabric[0].CreateRoom();
            this[x, y].IsVisited = true;
            if (x > 2 && !Elements[x - 2, y].IsVisited) neighbours.Add(new Point(x - 2, y));
            if (y > 2 && !Elements[x, y - 2].IsVisited) neighbours.Add(new Point(x, y - 2));
            if (x < Height - 3 && !Elements[x + 2, y].IsVisited) neighbours.Add(new Point(x + 2, y));
            if (y < Width - 3 && !Elements[x, y + 2].IsVisited) neighbours.Add(new Point(x, y + 2));

            if (neighbours.Count > 0)
            {
                var chosenPoint = neighbours[rand.Next(neighbours.Count)];
                RemoveWall(currentPoint, chosenPoint);
                stack.Push(chosenPoint);
                currentPoint = chosenPoint;
            }
            else currentPoint = stack.Pop();
        } while (stack.Count > 0);

        Elements[currentPoint.X, currentPoint.Y] = new Player();
    }

    private void RemoveWall(Point a, Point b)
    {
        Point point;
        if (a.X == b.X)
        {
            if (a.Y < b.Y)
                point = new Point(a.X, a.Y + 1);
            else point = new Point(a.X, a.Y - 1);
        }
        else
        {
            if (a.X < b.X)
                point = new Point(a.X + 1, a.Y);
            else point = new Point(a.X - 1, a.Y);
        }

        this[point.X, point.Y] = fabric[0].CreateRoom();
        this[point.X, point.Y].IsVisited = true;
    }

    public static RectangularMaze GetMaze(int height, int width)
    {
        maze ??= new RectangularMaze(height, width);
        return maze;
    }

    public IMazeElement this[int i, int j]
    {
        get => Elements[i, j];
        private set => Elements[i, j] = value;
    }

    public IEnumerator<IMazeElement> GetEnumerator()
    {
        return Elements.Cast<IMazeElement>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}