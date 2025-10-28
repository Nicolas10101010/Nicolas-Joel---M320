using Car_Hire_Application.interfaces;

namespace Car_Hire_Application;

public class FlatRatePolicy(decimal rate) : IPricingPolicy
{
    public decimal CalculatePrice(int days)
    {
        return days * rate;
    }
}