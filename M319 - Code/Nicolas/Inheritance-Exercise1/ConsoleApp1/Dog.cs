namespace ConsoleApp1;

class Dog : Animal, IComparable<Dog>
{
    public int Age { get; set; }

    public Dog(string name, int age) : base(name)
    {
        Age = age;
    }

    public override void Speak()
    {
        Console.WriteLine($"{Name} bellt!");
    }

    public int CompareTo(Dog other)
    {
        return Age.CompareTo(other.Age);
    }
}