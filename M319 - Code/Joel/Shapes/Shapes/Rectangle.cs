namespace Shapes
{
    public class Rectangle : Shape
    {
        public double Width { get; private set; }
        public double Height { get; private set; }
        public override double Area => Width * Height;

        public Rectangle(string name, double width, double height) : base(name)
        {
            Width = width;
            Height = height;
        }

        public override void Scale(double factor)
        {
            Width *= factor;
            Height *= factor;
        }
    }
}