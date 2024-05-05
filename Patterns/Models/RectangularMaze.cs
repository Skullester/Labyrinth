using System.Collections;
using MazePrinter;

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
        InitializeElements();
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

    private void InitializeElements()
    {
        Elements = new IMazeElement[Width, Length];
        Random rand = new();
        RoomCreator[] fabric = [new DefaultRoomCreator(), new RoomSpikesCreator()];
        IWall wall;
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Length; j++)
            {
                if (i == 0 || i == Width - 1 || j == 0 || j == Length - 1)
                {
                    // wall = new ExternalWall();
                    this[i, j] = new ExternalWall();
                }
                else if (rand.Next(10) % 2 == 0)
                {
                    wall = new InternalWall(100);
                }
                else
                {
                    var index = rand.Next(2);
                    this[i, j] = fabric[index].CreateRoom();
                    // continue;
                }

                // this[i, j] = wall.Clone();
            }
        }
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