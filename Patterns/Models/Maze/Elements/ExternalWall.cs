namespace Patterns;

public class ExternalWall : IWall
{
    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;

    public override string ToString()
    {
        return "#";
    }

    public bool IsVisited { get; set; }
}