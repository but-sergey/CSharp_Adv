using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        
        public const int min_size = 5;
        public const int max_size = 20;
        public const int min_speed = 1;
        public const int max_speed = 10;
        public const int stars_count = 75;

        public static int Width { get; set; }
        public static int Height { get; set; }

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

            var game_objects = new List<VisualObject>();
            
            for (var i = 0; i < stars_count; i++)
            {
                game_objects.Add(new Star(new Point(rand.Next(0, Width), rand.Next(0, Height)),
                                            new Point(rand.Next(-max_speed, -min_speed), 0),
                                            rand.Next(min_size, max_size)));
            }

            __GameObjects = game_objects.ToArray();
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
