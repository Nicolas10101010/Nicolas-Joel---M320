namespace Shapes
{
    public class Circle : Shape
    {
        public double Radius { get; private set; }
        public override double Area => Math.PI * Radius * Radius;

        public Circle(string name, double radius) : base(name)
        {
            Radius = radius;
        }

        public override void Scale(double factor)
        {
            Radius *= factor;
        }
    }
}