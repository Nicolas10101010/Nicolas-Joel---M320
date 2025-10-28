using Car_Hire_Application.interfaces;

namespace Car_Hire_Application;

public class CarRepository : IRepository<Car>
{
    private readonly List<Car> _cars = [];

    public void Add(Car item)
    {
        _cars.Add(item);
    }

    public Car? Get(int id)
    {
        return _cars.Find(c => c.Id == id);
    }

    public List<Car> GetAll()
    {
        return _cars;
    }
}