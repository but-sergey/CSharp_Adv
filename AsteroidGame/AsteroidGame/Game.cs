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

        private const int frame_timeout = 10;
        
        public const int star_min_size = 5;
        public const int star_max_size = 15;
        public const int star_min_speed = 1;
        public const int star_max_speed = 3;
        public const int star_count = 75;

        public const int asteroid_size = 40;
        public const int asteroid_speed = 7;
        public const int asteroid_count = 5;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = frame_timeout };
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static VisualObject[] __GameObjects;
        private static Bullet __Bullet;

        public static void Load()
        {
            Random rand = new Random();

            var game_objects = new List<VisualObject>();

            for (var i = 0; i < star_count; i++)
            {
                game_objects.Add(new Star(new Point(rand.Next(0, Width), rand.Next(0, Height)),
                                            new Point(rand.Next(-star_max_speed, -star_min_speed), 0),
                                            rand.Next(star_min_size, star_max_size)));
            }

            for (var i = 0; i < asteroid_count; i++)
            {
                game_objects.Add(new Asteroid(new Point(rand.Next(100, Width), rand.Next(0, Height)),
                                            new Point(-asteroid_speed, 0),
                                            asteroid_size));
            }

            __GameObjects = game_objects.ToArray();
            __Bullet = new Bullet(200);
        }

        public static void Draw()
        {
            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var visual_object in __GameObjects)
                visual_object.Draw(g);

            __Bullet.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object.Update();

            __Bullet.Update();
            if (__Bullet.Position.X > Width)
                __Bullet = new Bullet(200);
        }
    }
}
