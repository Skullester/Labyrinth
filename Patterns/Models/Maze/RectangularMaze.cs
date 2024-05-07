using System.Collections;

namespace Patterns;

public class RectangularMaze : IMaze
{
    public int Length { get; }
    public int Width { get; }
    private static RectangularMaze? maze;

    public IMazeElement[,] Elements { get; private set; } = null!;

    private RectangularMaze(int length, int width)
    {
        Length = length;
        Width = width;
        InitializeExternalWalls();
        InitializeObjects();
    }

    private (int, int) GetNextStepIndexes(Direction direction, (int i, int j) currentIndexies)
    {
        var i = currentIndexies.i;
        var j = currentIndexies.j;
        return direction switch
        {
            Direction.Right => (i, ++j),
            Direction.Down => (++i, j),
            Direction.Left => (i, --j),
            _ => (--i, j)
        };
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }

    private void InitializeExternalWalls()
    {
        Elements = new IMazeElement[Width, Length];
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Length; j++)
            {
                if (i == 0 || i == Width - 1 || j == 0 || j == Length - 1)
                    this[i, j] = new ExternalWall();
                else this[i, j] = new InternalWall(100);
            }
        }
    }

    private void InitializeObjects()
    {
        /*var rand = new Random();
        int i = 1, j = 1; //rand.Next(1, Length);
        Elements[i, j] = new Player();
        var stack = new Stack<(int i, int j)>();
        RoomCreator[] fabric = [new DefaultRoomCreator() /*, new RoomSpikesCreator()#1#];
        var coords = (i, j);
        do
        {
            var direction = (Direction)rand.Next(4);
            coords = GetNextStepIndexes(direction, coords);
            i = coords.i;
            j = coords.j;
            var nextStep = this[i, j];
            if (nextStep is ExternalWall or IRoom or Player)
            {
                coords = stack.Pop();
            }
            else
            {
                stack.Push(coords);
                var index = rand.Next(fabric.Length);
                this[i, j] = fabric[index].CreateRoom();
            }
        } while (stack.Count > 0);*/
    }

    public static RectangularMaze GetMaze(int length, int width)
    {
        maze ??= new RectangularMaze(length, width);
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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}