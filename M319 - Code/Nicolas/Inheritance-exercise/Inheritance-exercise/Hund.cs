namespace Inheritance_Exercises;

public class Hund : Tier
{
    public void Bellen() => Console.WriteLine($"{Name} bellt: Wuff!");

    public override void LautGeben()
    {
        Console.WriteLine($"{Name} sagt: Wuff Wuff!");
    }
}