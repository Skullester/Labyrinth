using Patterns;

namespace MazePrinter;

public static class Extensions
{
    public static (int, int) ParseArrayToTuple(this int[] arr)
    {
        if (arr.Length != 2) throw new InvalidOperationException("Число элементов в массиве должно равняться двум!");
        return (arr[0], arr[1]);
    }

    public static IEnumerable<char> ParseToChar(this IEnumerable<IMazeElement> elements, IMazeFormatter formatter)
    {
        return elements.Select(formatter.Format);
    }
}