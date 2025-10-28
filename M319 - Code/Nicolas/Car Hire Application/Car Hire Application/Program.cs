namespace Car_Hire_Application;

internal abstract class Program
{
    private static void Main()
    {
        var repo = new CarRepository();
        repo.Add(new Car(1, "VW Golf"));
        repo.Add(new Car(2, "BMW 3er"));
        repo.Add(new Car(3, "Audi A4"));

        var service = new RentalService(repo);

        var flatRate = new FlatRatePolicy(50);
        var discount = new DiscountPolicy(50, 7, 0.15m);

        Console.WriteLine("Verfügbare Autos:");
        foreach (var car in service.GetAvailableCars())
            Console.WriteLine(car.GetDescription());

        var price1 = service.RentCar(1, 5, flatRate);
        Console.WriteLine($"\nPreis (Flat Rate, 5 Tage): {price1} CHF");

        var price2 = service.RentCar(2, 10, discount);
        Console.WriteLine($"Preis (Discount, 10 Tage): {price2} CHF");

        service.ReturnCar(1);
        Console.WriteLine("\nAuto 1 zurückgegeben");
    }
}