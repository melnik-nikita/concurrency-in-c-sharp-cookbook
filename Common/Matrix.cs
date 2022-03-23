namespace Common;
public abstract class MatrixBase
{
    public abstract string Name { get; }
    public bool IsInvertible { get; set; }
    public virtual void Rotate(float degrees) => Console.WriteLine($"Rotating matrix '{Name}' on {degrees} degrees");
    public virtual void Invert() => Console.WriteLine($"Matrix '{Name}' Inverted");
}

public class Matrix : MatrixBase
{

    public override string Name { get;}

    public Matrix(string name, bool isInvertible = false)
    {
        Name = name;
        IsInvertible = isInvertible;
    }
}
