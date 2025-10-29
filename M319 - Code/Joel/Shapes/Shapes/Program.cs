using Shapes;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var bag = new ShapeBag();
        bag.Add(new Circle("c1", 2));
        bag.Add(new Rectangle("r1", 3, 4));
        bag.Add(new Circle("c2", 1.5));

        foreach (var s in bag.All().OrderBy(x => x.Area))
            Console.WriteLine($"{s.Name} {s.Area:F2} {s.GetType().Name}");
    }
}