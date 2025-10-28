namespace Inheritance_Exercises;

public class Vogel : Tier
{
    public override void LautGeben()
    {
        Console.WriteLine($"{Name} zwitschert: Piep Piep!");
    }
}