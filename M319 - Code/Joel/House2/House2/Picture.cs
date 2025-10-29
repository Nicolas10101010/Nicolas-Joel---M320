using System.Drawing;
using System.Threading.Tasks;

namespace HouseDemo
{
    public sealed class Picture
    {
        private readonly CanvasForm _canvas;

        // Bildelemente
        private Square _wall;
        private Square _window;
        private Triangle _roof;
        private Circle _sun;
        private Cloud _cloud;
        private Person _person;
        private Square _ground;

        public Picture(CanvasForm canvas) => _canvas = canvas;

        public async void Draw()
        {
            // Hintergrund / Boden
            _ground = new Square(0, _canvas.ClientSize.Height - 80, _canvas.ClientSize.Width, Color.FromArgb(90, 200, 120))
            {
                Height = 80
            };
            _ground.Filled = true;

            // Haus
            _wall = new Square(260, _canvas.ClientSize.Height - 80 - 140, 160, Color.Red);
            _window = new Square(_wall.X + 25, _wall.Y + 35, 35, Color.Black);
            _roof = new Triangle(_wall.X - 20, _wall.Y - 70, _wall.Width + 40, 70, Color.Green);
            _sun = new Circle(560, 40, 70, Color.Gold);
            _cloud = new Cloud(-180, 70, 60);
            _person = new Person(40, _canvas.ClientSize.Height - 80) { Height = 70 };

            // Registrieren
            _canvas.Add(_ground);
            _canvas.Add(_sun);
            _canvas.Add(_cloud);
            _canvas.Add(_wall);
            _canvas.Add(_roof);
            _canvas.Add(_window);
            _canvas.Add(_person);

            // Animationen gemaess Auftrag:
            // 1) Wolke zieht ueber den Himmel
            await _canvas.AnimateAsync(() =>
            {
                _cloud.Move(2, 0);
                return _cloud.Right < _canvas.ClientSize.Width + 40;
            }, 12);

            // 2) Person geht zum Haus (Tuer ungefaehr mittig unten)
            int targetX = _wall.X + _wall.Width / 2;
            await _canvas.AnimateAsync(() =>
            {
                if (_person.X < targetX) _person.Move(2, 0);
                return _person.X < targetX;
            }, 12);

            // 3) Sonne geht langsam runter (Sonnenuntergang)
            int targetY = _canvas.ClientSize.Height - 80 - _sun.Height / 2;
            await _canvas.AnimateAsync(() =>
            {
                if (_sun.Y < targetY) _sun.Move(0, 1);
                return _sun.Y < targetY;
            }, 20);
        }

        // Optional: Modi wie im BlueJ-Original
        public void SetBlackWhite()
        {
            _wall.Color = Color.Gray;
            _window.Color = Color.White;
            _roof.Color = Color.DarkGray;
            _sun.Color = Color.LightGray;
            _ground.Color = Color.Gray;
            _canvas.Redraw();
        }

        public void SetColor()
        {
            _wall.Color = Color.Red;
            _window.Color = Color.Black;
            _roof.Color = Color.Green;
            _sun.Color = Color.Gold;
            _ground.Color = Color.FromArgb(90, 200, 120);
            _canvas.Redraw();
        }
    }
}