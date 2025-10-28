namespace Inheritance_Exercises;

class Program
{
    private static void Main(string[] args)
    {
        var hund = new Hund { Name = "Bello" };
        hund.Essen();
        hund.Bellen();
        hund.LautGeben();

        Console.WriteLine();

        var katze = new Katze { Name = "Minka" };
        katze.Essen();
        katze.Miauen();
        katze.LautGeben();

        Console.WriteLine();
        
        List<Tier> tiere = new List<Tier>
        {
            new Hund { Name = "Rex" },
            new Katze { Name = "Luna" },
            new Vogel { Name = "Tweety" }
        };

        Console.WriteLine("=== Polymorphismus-Demo ===");
        foreach (var tier in tiere)
        {
            tier.LautGeben(); 
        }

        Console.WriteLine();

        //Overloarding Beispiel
        var rechner = new Rechner();
        Console.WriteLine($"Addiere 2 + 3 = {rechner.Add(2, 3)}");
        Console.WriteLine($"Addiere 2.5 + 3.5 = {rechner.Add(2.5, 3.5)}");
        Console.WriteLine($"Addiere 1 + 2 + 3 = {rechner.Add(1, 2, 3)}");
    }
}