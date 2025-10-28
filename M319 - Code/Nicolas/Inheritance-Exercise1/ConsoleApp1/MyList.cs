namespace ConsoleApp1;

abstract class MyList<T>
{
    public abstract void AddElement(T item);
    public abstract T Get(int index);
    public abstract int Size();
}