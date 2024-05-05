namespace Patterns;

public class ExternalWall : IWall
{
    public ExternalWall()
    {
    }

    public IWall Clone() => (MemberwiseClone() as ExternalWall)!;
}