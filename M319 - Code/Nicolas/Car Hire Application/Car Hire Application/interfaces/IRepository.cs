namespace Car_Hire_Application.interfaces;

public interface IRepository<T>
{
    void Add(T item);
    Car? Get(int id);
    List<T> GetAll();
}