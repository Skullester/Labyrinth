using System.Collections;

namespace Patterns;

public class Room : IRoom
{
    public override string ToString()
    {
        return " ";
    }

    public bool IsVisited { get; set; }
}