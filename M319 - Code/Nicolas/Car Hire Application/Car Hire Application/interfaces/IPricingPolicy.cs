namespace Car_Hire_Application.interfaces;


public interface IPricingPolicy
{
    decimal CalculatePrice(int days);
}