using Car_Hire_Application.interfaces;

namespace Car_Hire_Application;

public class Car(int id, string model) : IRentable
{
    public int Id { get; set; } = id;
    private string Model { get; set; } = model;
    public bool IsAvailable { get; set; } = true;

    public string GetDescription()
    {
        return $"Car: {Model}";
    }
}