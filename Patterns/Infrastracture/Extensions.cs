using Patterns;

namespace MazePrinter;

public static class Extensions
{
    public static (int, int) ParseArrayToTuple(this int[] arr)
    {
        if (arr.Length != 2) throw new InvalidOperationException("Число элементов в массиве должно равняться двум!");
        return (arr[0], arr[1]);
    }

    /*
    public static (int, int) GetIndexesOfBorderElement<T>(this T[,] array, Random? rand = default)
    {
        rand ??= new Random();
        var rows = array.GetLength(0);
        var columns = array.GetLength(1);
        var i = rand.Next(rows);
        int j;
        if (i != 0 || i != rows - 1)
        {
            var index = rand.Next(2);
            j = index == 0 ? 0 : columns - 1;
        }
        else
        {
            j = rand.Next(columns);
        }

        return (i, j);
    }
    */

    public static IEnumerable<char> ParseToChar(this IEnumerable<IMazeElement> elements, IMazeFormatter formatter)
    {
        foreach (var element in elements)
        {
            yield return formatter.Format(element);
        }
    }
}