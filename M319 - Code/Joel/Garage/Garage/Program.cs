using System;
using System.Collections.Generic;
using Garage;

class Program
{
    static void Main()
    {
        var jobs = new List<Vehicle>
        {
            new Car("A1", 80m),
            new Truck("T1", 90m)
        };

        foreach (var v in jobs)
            Console.WriteLine($"{v} | 3h: {v.Estimate(3m):F2} CHF");

        Console.WriteLine($"Auto 3h + Teile 50: {jobs[0].Estimate(3m, 50m):F2} CHF");

        jobs[0].MarkRepaired();
        Console.WriteLine(jobs[0]);
    }
}