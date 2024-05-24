using System.Collections;
using System.Drawing;

namespace Patterns;

public class RectangularMaze : IMaze
{
    public int Height { get; }
    public int Width { get; }
    private static RectangularMaze? maze;

    public IMazeElement[,] Elements { get; private set; } = null!;

    private readonly RoomCreator[] fabric =
    [
        new DefaultRoomCreator(),
        new ExitRoomCreator(),
    ];

    private RectangularMaze(int height, int width)
    {
        Height = height;
        Width = width;
        InitializeExternalWalls();
        InitializeObjects();
        FindFurthestExit();
    }

    private void FindFurthestExit()
    {
        var furthest = Point.Empty;
        for (var x = 1; x < Elements.GetLength(0); x++)
        {
            if (Elements[x, Width - 2].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(x, Width - 2);
            if (Elements[x, 1].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(x, 1);
        }

        for (var y = 1; y < Elements.GetLength(1); y++)
        {
            if (Elements[Height - 2, y].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(Height - 2, y);
            if (Elements[1, y].Distance > Elements[furthest.X, furthest.Y].Distance)
                furthest = new Point(1, y);
        }

        Point point = default;
        if (furthest.X == 1) point = new Point(furthest.X - 1, furthest.Y);
        else if (furthest.Y == 1) point = new Point(furthest.X, furthest.Y - 1);
        else if (furthest.X == Height - 2) point = new Point(furthest.X + 1, furthest.Y);
        else if (furthest.Y == Width - 2) point = new Point(furthest.X, furthest.Y + 1);
        this[point.X, point.Y] = fabric[1].CreateRoom();
    }

    private void InitializeExternalWalls()
    {
        Elements = new IMazeElement[Height, Width];
        var exWall = new ExternalWall();
        var inWall = new InternalWall(100);
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (i == 0 || i == Height - 1 || j == 0 || j == Width - 1)
                    this[i, j] = exWall.Clone();
                else this[i, j] = inWall.Clone();
            }
        }
    }

    private void InitializeObjects()
    {
        var rand = new Random();

        Point[] startPositions =
        [
            new Point(1, 1),
            new Point(Height - 2, 1),
            new Point(1, Width - 2),
            new Point(Height - 2, Width - 2)
        ];
        var startPoint = startPositions[rand.Next(startPositions.Length)];
        var currentPoint = startPoint;
        var stack = new Stack<Point>();
        do
        {
            var neighbours = new List<Point>();
            var x = currentPoint.X;
            var y = currentPoint.Y;
            var distance = this[x, y].Distance;
            this[x, y] = fabric[0].CreateRoom();
            this[x, y].Distance = distance;
            this[x, y].IsVisited = true;
            if (x > 2 && !Elements[x - 2, y].IsVisited) neighbours.Add(new Point(x - 2, y));
            if (y > 2 && !Elements[x, y - 2].IsVisited) neighbours.Add(new Point(x, y - 2));
            if (x < Height - 1 - 2 && !Elements[x + 2, y].IsVisited) neighbours.Add(new Point(x + 2, y));
            if (y < Width - 1 - 2 && !Elements[x, y + 2].IsVisited) neighbours.Add(new Point(x, y + 2));

            if (neighbours.Count > 0)
            {
                var chosenPoint = neighbours[rand.Next(neighbours.Count)];
                RemoveWall(currentPoint, chosenPoint);
                this[chosenPoint.X, chosenPoint.Y].Distance = this[currentPoint.X, currentPoint.Y].Distance + 1;
                stack.Push(chosenPoint);
                currentPoint = chosenPoint;
            }
            else currentPoint = stack.Pop();
        } while (stack.Count > 0);

        Elements[startPoint.X, startPoint.Y] = new Player();
    }

    private void RemoveWall(Point a, Point b)
    {
        Point point;
        if (a.X == b.X)
            point = a.Y < b.Y ? new Point(a.X, a.Y + 1) : new Point(a.X, a.Y - 1);
        else
            point = a.X < b.X ? new Point(a.X + 1, a.Y) : new Point(a.X - 1, a.Y);

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