using Car_Hire_Application.interfaces;

namespace Car_Hire_Application;

public class RentalService(IRepository<Car> repo)
{
    public List<Car> GetAvailableCars()
    {
        return repo.GetAll().Where(c => c.IsAvailable).ToList();
    }

    public decimal RentCar(int carId, int days, IPricingPolicy policy)
    {
        var car = repo.Get(carId);
        if (car is not { IsAvailable: true }) return 0;
        car.IsAvailable = false;
        return policy.CalculatePrice(days);
    }

    public void ReturnCar(int carId)
    {
        var car = repo.Get(carId);
        if (car != null)
            car.IsAvailable = true;
    }
}