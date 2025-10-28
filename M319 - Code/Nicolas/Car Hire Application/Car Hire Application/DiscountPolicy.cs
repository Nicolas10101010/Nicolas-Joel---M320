using Car_Hire_Application.interfaces;

namespace Car_Hire_Application;

public class DiscountPolicy(decimal rate, int afterDays, decimal discount) : IPricingPolicy
{
    public decimal CalculatePrice(int days)
    {
        var total = days * rate;
        if (days >= afterDays) total *= (1 - discount);
        return total;
    }
}