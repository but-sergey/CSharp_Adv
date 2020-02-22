using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        private static int _MinSize = 5;
        private static int _MaxSize = 20;
        private static int _MinSpeed = 1;
        private static int _MaxSpeed = 10;
        //private static Image _Star = Image.FromFile("..\\..\\star.jpg");

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static int MinSize { get { return _MinSize; } }
        public static int MaxSize { get { return _MaxSize; } }
        public static int MinSpeed { get { return _MinSpeed; } }
        public static int MaxSpeed { get { return _MaxSpeed; } }
        //public static Image Star { get { return _Star; } }

        //static Game()
        //{

        //}

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = 100 };
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static VisualObject[] __GameObjects;

        public static void Load()
        {
            Random rand = new Random();

            __GameObjects = new VisualObject[75];
            for (var i = 0; i < __GameObjects.Length; i++)
            {
                __GameObjects[i] = new Star(new Point(rand.Next(0, Width), rand.Next(0, Height)),
                                            new Point(rand.Next(-_MaxSpeed, -_MinSpeed), 0),
                                            rand.Next(_MinSize, _MaxSize));
            }
        }

        public static void Draw()
        {
            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var visual_object in __GameObjects)
                visual_object.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object.Update();
        }
    }
}
