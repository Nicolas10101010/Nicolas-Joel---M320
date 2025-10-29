using System.Drawing;

namespace HouseDemo
{
    public abstract class Shape : IDrawable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 40;
        public int Height { get; set; } = 40;
        public Color Color { get; set; } = Color.Black;
        public bool Filled { get; set; } = true;
        public bool Visible { get; set; } = true;

        protected Shape(int x = 0, int y = 0, int w = 40, int h = 40, Color? color = null)
        {
            X = x; Y = y; Width = w; Height = h;
            if (color.HasValue) Color = color.Value;
        }

        public abstract void Draw(Graphics g);

        public void Move(int dx, int dy) { X += dx; Y += dy; }
        public void MoveTo(int x, int y) { X = x; Y = y; }
    }

    public sealed class Square : Shape
    {
        public Square(int x, int y, int size, Color color) : base(x, y, size, size, color) { }

        public override void Draw(Graphics g)
        {
            if (Filled)
            {
                using (var brush = new SolidBrush(Color))
                    g.FillRectangle(brush, X, Y, Width, Height);
            }
            else
            {
                using (var pen = new Pen(Color, 1))
                    g.DrawRectangle(pen, X, Y, Width, Height);
            }
        }
    }

    public sealed class Circle : Shape
    {
        public Circle(int x, int y, int diameter, Color color) : base(x, y, diameter, diameter, color) { }

        public override void Draw(Graphics g)
        {
            if (Filled)
            {
                using (var brush = new SolidBrush(Color))
                    g.FillEllipse(brush, X, Y, Width, Height);
            }
            else
            {
                using (var pen = new Pen(Color, 1))
                    g.DrawEllipse(pen, X, Y, Width, Height);
            }
        }
    }

    public sealed class Triangle : Shape
    {
        public Triangle(int x, int y, int w, int h, Color color) : base(x, y, w, h, color) { }

        public override void Draw(Graphics g)
        {
            var p1 = new Point(X, Y + Height);
            var p2 = new Point(X + Width, Y + Height);
            var p3 = new Point(X + Width / 2, Y);
            var pts = new[] { p1, p2, p3 };

            if (Filled)
            {
                using (var brush = new SolidBrush(Color))
                    g.FillPolygon(brush, pts);
            }
            else
            {
                using (var pen = new Pen(Color, 1))
                    g.DrawPolygon(pen, pts);
            }
        }
    }
}