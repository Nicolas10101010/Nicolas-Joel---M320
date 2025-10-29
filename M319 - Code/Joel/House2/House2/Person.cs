using System.Drawing;

namespace HouseDemo
{
    public sealed class Person : IDrawable
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Visible { get; set; } = true;
        public Color BodyColor { get; set; } = Color.SaddleBrown;
        public Color HeadColor { get; set; } = Color.Bisque;
        public int Height { get; set; } = 60;

        public Person(int x, int y) { X = x; Y = y; }

        public void Move(int dx, int dy) { X += dx; Y += dy; }
        public void MoveTo(int x, int y) { X = x; Y = y; }

        public void Draw(Graphics g)
        {
            int h = Height;
            int head = h / 4;
            int body = h / 2;
            int leg = h - head - body;
            int cx = X; int cy = Y;

            using (var headBrush = new SolidBrush(HeadColor))
                g.FillEllipse(headBrush, cx - head / 2, cy - h, head, head);
            using (var bodyBrush = new SolidBrush(BodyColor))
                g.FillRectangle(bodyBrush, cx - head / 3, cy - h + head, (int)(head * 0.66), body);
            using (var pen = new Pen(BodyColor, 2))
            {
                g.DrawLine(pen, cx, cy - h + head + body / 3, cx - head, cy - h + head + body / 2);
                g.DrawLine(pen, cx, cy - h + head + body / 3, cx + head, cy - h + head + body / 2);
                g.DrawLine(pen, cx, cy - leg, cx - head / 2, cy);
                g.DrawLine(pen, cx, cy - leg, cx + head / 2, cy);
            }
        }
    }
}