using ConsoleApp1;

class Program
{
    static void Main()
    {
        Dog d1 = new Dog("Bello", 5);
        Dog d2 = new Dog("Rex", 2);

        d1.Speak();
        d2.Speak();

        Console.WriteLine("Älterer Hund: " + (d1.CompareTo(d2) > 0 ? d1.Name : d2.Name));

        SimpleList<string> myList = new SimpleList<string>();
        myList.AddElement("Hallo");
        myList.AddElement("Welt");

        Console.WriteLine($"Liste: {myList.Get(0)} {myList.Get(1)}");
    }
}
;