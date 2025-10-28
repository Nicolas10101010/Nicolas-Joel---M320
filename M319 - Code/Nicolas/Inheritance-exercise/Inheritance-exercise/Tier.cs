namespace Inheritance_Exercises;

public abstract class Tier
{
    public string Name { get; init; }

    public virtual void LautGeben()
    {
        Console.WriteLine($"{Name} macht ein undefiniertes Geräusch");
    }

    public void Essen() => Console.WriteLine($"{Name} ist am essen");
}