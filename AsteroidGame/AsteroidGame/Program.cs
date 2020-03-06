using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Console.WriteLine($"{DateTime.Now} Игра запущена");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form();
            form.Width = 1200;
            form.Height = 600;

            form.Show();

            Game.Initialize(form);
            Game.Load();
            Game.Draw();

            Application.Run(form);
        }
    }
}
