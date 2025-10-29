using System;
using System.Windows.Forms;

namespace HouseDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var canvas = CanvasForm.GetCanvas(); // Singleton
            // Startbild zeichnen + Animation starten
            var picture = new Picture(canvas);
            picture.Draw();
            Application.Run(canvas);
        }
    }
}