namespace ConsoleApp1;

class SimpleList<T> : MyList<T>
{
    private List<T> data = new List<T>();

    public override void AddElement(T item)
    {
        data.Add(item);
    }

    public override T Get(int index)
    {
        return data[index];
    }

    public override int Size()
    {
        return data.Count;
    }
}