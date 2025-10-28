namespace Car_Hire_Application.interfaces;

public interface IPaymentGateway
{
    void Charge(decimal amount);
}