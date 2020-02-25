using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;
using AsteroidGame.VisualObjects.Interfaces;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private const int frame_timeout = 10;

        public const int moon_height = -100;
        public const int moon_min_size = 200;
        public const int moon_max_size = 700;
        public const int moon_speed = 1;
        
        public const int star_min_size = 3;
        public const int star_max_size = 15;
        public const int star_min_speed = 1;
        public const int star_max_speed = 5;
        public const int star_count = 50;
        public static Pen star_pen = Pens.DarkGray;

        public const int asteroid_size = 60;
        public const int asteroid_speed = 3;
        public const int asteroid_count = 10;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            if (Width > 1500 || Width < 0)
                throw new ArgumentOutOfRangeException("Ширина игрового поля меньше 0 или более 1500");
            if (Height > 1000 || Height < 0)
                throw new ArgumentOutOfRangeException("Высота игрового поля меньше 0 или более 1000");

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

            game_objects.Add(new Moon(new Point(Game.Width, moon_height),
                          new Point(-moon_speed, 0), rand.Next(moon_min_size, moon_max_size)));

            for (var i = 0; i < star_count; i++)
            {
                game_objects.Add(new Star(new Point(rand.Next(0, Width), rand.Next(0, Height)),
                                            new Point(rand.Next(-star_max_speed, -star_min_speed), 0),
                                            rand.Next(star_min_size, star_max_size), star_pen));
            }

            for (var i = 0; i < asteroid_count; i++)
            {
                game_objects.Add(new Asteroid(new Point(rand.Next(500, 500 + Width), rand.Next(0, Height)),
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
                visual_object?.Draw(g);

            __Bullet.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            Random rand = new Random();

            foreach (var visual_object in __GameObjects)
                visual_object?.Update();

            __Bullet.Update();
            if (__Bullet.Position.X > Width)
                __Bullet = new Bullet(new Random().Next(Height));

            for(var i = 0; i < __GameObjects.Length; i++)
            {
                var obj = __GameObjects[i];
                if(obj is ICollision)
                {
                    var collision_object = (ICollision)obj;
                    if(__Bullet.CheckCollision(collision_object))
                    {
                        __Bullet = new Bullet(new Random().Next(Height));
                        __GameObjects[i] = new Asteroid(new Point(rand.Next(Width, 2 * Width), rand.Next(0, Height)),
                                            new Point(-asteroid_speed, 0),
                                            asteroid_size);
                        //MessageBox.Show("Астероид уничтожен!", "Столкновение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
