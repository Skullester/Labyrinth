using System.Collections;

namespace Patterns;

public class Room : IRoom
{
    public bool IsVisited { get; set; }
    public int Distance { get; set; }
}