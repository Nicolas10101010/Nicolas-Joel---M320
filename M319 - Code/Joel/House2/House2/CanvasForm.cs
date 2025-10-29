using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseDemo
{
    public sealed class CanvasForm : Form
    {
        private static CanvasForm _singleton;
        public static CanvasForm GetCanvas()
        {
            if (_singleton == null)
                _singleton = new CanvasForm();
            return _singleton;
        }

        private readonly List<IDrawable> _items = new List<IDrawable>();
        internal IReadOnlyList<IDrawable> Items => _items.AsReadOnly();

        private CanvasForm()
        {
            Text = "C# Picture Demo";
            ClientSize = new Size(800, 480);
            BackColor = Color.White;
            DoubleBuffered = true;      // fuer saubere Animation
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Add(IDrawable d)
        {
            if (!_items.Contains(d)) _items.Add(d);
            Invalidate();
        }

        public void Remove(IDrawable d)
        {
            _items.Remove(d);
            Invalidate();
        }

        public void Redraw() => Invalidate();

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var d in _items)
                if (d.Visible) d.Draw(e.Graphics);
        }

        // Kleine Helfer fuer Animationen
        public async Task AnimateAsync(Func<bool> step, int delayMs = 10)
        {
            // step() soll Positionen aendern und true zurueckgeben, solange weiter animiert wird
            while (step())
            {
                Invalidate();
                await Task.Delay(delayMs);
                Application.DoEvents();
            }
            Invalidate();
        }
    }

    public interface IDrawable
    {
        bool Visible { get; set; }
        void Draw(Graphics g);
    }
}