namespace Car_Hire_Application.interfaces;


public interface IRentable
{
    string GetDescription();
    bool IsAvailable { get; set; }
}