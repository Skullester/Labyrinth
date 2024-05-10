namespace Patterns;

public class ExternalWall : IWall
{
    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;

    public bool IsVisited { get; set; }
    public int Distance { get; set; }
}