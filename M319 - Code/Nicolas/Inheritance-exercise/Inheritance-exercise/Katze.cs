namespace Inheritance_Exercises;

public class Katze : Tier
{
    public void Miauen() => Console.WriteLine($"{Name} miaut: Miau!");

    public override void LautGeben()
    {
        Console.WriteLine($"{Name} sagt: Miau Miau!");
    }
}