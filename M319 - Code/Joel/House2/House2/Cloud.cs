using System.Collections.Generic;
using System.Drawing;

namespace HouseDemo
{
    public sealed class Cloud : IDrawable
    {
        private readonly List<Circle> _parts = new List<Circle>();
        public bool Visible { get; set; } = true;

        public Cloud(int x, int y, int size, Color? color = null)
        {
            Color c = color ?? Color.LightGray;
            _parts.Add(new Circle(x, y, size, c));
            _parts.Add(new Circle(x + size / 2, y - size / 3, (int)(size * 0.9), c));
            _parts.Add(new Circle(x + size, y, size, c));
            _parts.Add(new Circle(x + size / 3, y, (int)(size * 1.1), c));
        }

        public void Move(int dx, int dy)
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                _parts[i].X += dx;
                _parts[i].Y += dy;
            }
        }

        public int Right
        {
            get
            {
                var last = _parts[_parts.Count - 1];
                return last.X + last.Width;
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < _parts.Count; i++)
                if (Visible) _parts[i].Draw(g);
        }
    }
}