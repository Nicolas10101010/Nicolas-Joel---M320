using System.Collections.Generic;

namespace Shapes
{
    public class ShapeBag
    {
        private readonly List<Shape> shapes = new();

        public void Add(Shape s)
        {
            if (shapes.Exists(x => x.Name == s.Name)) return;
            shapes.Add(s);
        }

        public IEnumerable<Shape> All() => shapes;
    }
}