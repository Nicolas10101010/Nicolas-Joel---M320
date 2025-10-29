namespace Shapes
{
    public abstract class Shape
    {
        public string Name { get; }
        public abstract double Area { get; }

        protected Shape(string name)
        {
            Name = name;
        }

        public abstract void Scale(double factor);
    }
}