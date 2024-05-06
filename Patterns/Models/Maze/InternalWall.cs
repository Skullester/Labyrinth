namespace Patterns;

public class InternalWall : IWall
{
    private float durability;

    public float Durability
    {
        get => durability;
        set
        {
            if (value < 0) throw new ArgumentException("Ошибка");
            durability = value;
        }
    }

    public InternalWall(float durability)
    {
        Durability = durability;
    }

    public IWall Clone() => (MemberwiseClone() as InternalWall)!;

    public override string ToString()
    {
        return "*";
    }
}