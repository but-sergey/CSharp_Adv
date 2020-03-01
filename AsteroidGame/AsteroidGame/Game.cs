using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;
using AsteroidGame.VisualObjects.Interfaces;
using System.IO;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private const string log_name = "game_log.txt"; 
        
        private const int frame_timeout = 10;

        public static Random rand = new Random();

        public const int moon_height = -100;
        public const int moon_min_size = 200;
        public const int moon_max_size = 900;
        public const int moon_speed = 2;
        
        public const int star_min_size = 3;
        public const int star_max_size = 10;
        public const int star_min_speed = 10;
        public const int star_max_speed = 20;
        public const int star_count = 75;
        public static Pen star_pen = Pens.DarkGray;
        public static Brush star_brush = Brushes.DarkGray;

        public const int asteroid_size = 60;
        public const int asteroid_min_speed = 3;
        public const int asteroid_max_speed = 6;
        public const int asteroid_count = 10;

        public const int bullet_speed = 10;

        private static SpaceShip __Ship;
        private static VisualObject[] __GameObjects;
        private static List<Bullet> __Bullets = new List<Bullet>();
        private static Back[] __Background = new Back[2];

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer timer = new Timer();

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            if (Width > 1500 || Width < 0)
                throw new ArgumentOutOfRangeException("Ширина игрового поля меньше 0 или более 1500");
            if (Height > 1000 || Height < 0)
                throw new ArgumentOutOfRangeException("Высота игрового поля меньше 0 или более 1000");

            __Context = BufferedGraphicsManager.Current;
            var g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            timer.Interval = frame_timeout;
            timer.Tick += OnTimerTick;

            form.KeyDown += OnFormKeyDown;
        }

        private static void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullets.Add(new Bullet(new Point(50, __Ship.Rect.Y + __Ship.Rect.Height / 2 - 1)));
                    break;
                case Keys.Up:
                    __Ship.MoveUp();
                    break;
                case Keys.Down:
                    __Ship.MoveDown();
                    break;
            }
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            Random rand = new Random();

            __Background[0] = new Back(new Point(0, 0), new Point(-1, 0), Properties.Resources.background.Size);
            __Background[1] = new Back(new Point(__Background[0].Rect.Right, 0), new Point(-1, 0), Properties.Resources.background.Size);

            var game_objects = new List<VisualObject>();

            game_objects.Add(new Moon(new Point(Game.Width, moon_height),
                          new Point(-moon_speed, 0), rand.Next(moon_min_size, moon_max_size)));

            for (var i = 0; i < star_count; i++)
            {
                game_objects.Add(new Star(new Point(rand.Next(0, Width), rand.Next(0, Height)),
                                            new Point(rand.Next(-star_max_speed, -star_min_speed), 0),
                                            rand.Next(star_min_size, star_max_size)));
            }

            for (var i = 0; i < asteroid_count; i++)
            {
                game_objects.Add(new Asteroid(new Point(rand.Next(750, 750 + Width), rand.Next(0, Height)),
                                            new Point(-rand.Next(asteroid_min_speed, asteroid_max_speed), 0),
                                            asteroid_size));
            }

            __GameObjects = game_objects.ToArray();
            __Bullets.Clear();
            __Ship = new SpaceShip(new Point(10, 400), new Point(5, 5), new Size(60, 30));

            __Ship.ShipCollisioned += OnShipCollisioned_LogConcole;
            __Ship.ShipCollisioned += OnShipCollisioned_LogFile;

            __Ship.ShipDestroyed += OnShipDestroyed;
            __Ship.ShipDestroyed += OnShipDestroyed_LogConsole;
            __Ship.ShipDestroyed += OnShipDestroyed_LogFile;

            timer.Start();
        }

        private static void OnShipCollisioned_LogConcole(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} Космический корабль столкнулся с астероидом! Energy = {__Ship.Energy}");
        }

        private static void OnShipDestroyed_LogConsole(object Sender, EventArgs E)
        {
            Console.WriteLine($"{DateTime.Now} Космический корабль уничтожен!");
        }

        private static void OnShipCollisioned_LogFile(object sender, EventArgs e)
        {
            File.AppendAllText(log_name, $"{DateTime.Now} Космический корабль столкнулся с астероидом! Energy = {__Ship.Energy}\n");
        }

        private static void OnShipDestroyed_LogFile(object Sender, EventArgs E)
        {
            File.AppendAllText(log_name, $"{DateTime.Now} Космический корабль уничтожен!\n\n");
        }

        private static void OnShipDestroyed(object Sender, EventArgs E)
        {
            timer.Stop();
            __Buffer.Graphics.Clear(Color.DarkBlue);
            __Buffer.Graphics.DrawString("Game Over!!!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.Red, 200, 100);
            __Buffer.Render();
        }

        public static void Draw()
        {
            if (__Ship.Energy <= 0) return;

            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            __Background[0].Draw(g);
            __Background[1].Draw(g);

            foreach (var visual_object in __GameObjects)
                visual_object?.Draw(g);

            __Ship.Draw(g);

            foreach(var bullet in __Bullets)
                bullet.Draw(g);

            g.DrawString($"Energy: {__Ship.Energy}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.White, 10, 10);

            __Buffer.Render();
        }

        public static void Update()
        {
            __Background[0].Update();
            __Background[1].Update();

            foreach (var visual_object in __GameObjects)
                visual_object?.Update();

            foreach(var bullet in __Bullets)
                bullet.Update();

            for(var i = 0; i < __GameObjects.Length; i++)
            {
                var obj = __GameObjects[i];
                if (obj is ICollision)
                {
                    var collision_object = (ICollision)obj;
                    if (__Ship.CheckCollision(collision_object))
                        //__GameObjects[i] = null;
                        __GameObjects[i] = new Asteroid(new Point(rand.Next(Width, 2 * Width), rand.Next(0, Height)),
                                            new Point(-rand.Next(asteroid_min_speed, asteroid_max_speed), 0),
                                            asteroid_size);
                    var bullets_to_remove = new List<Bullet>();
                    foreach(var bullet in __Bullets)
                        if (bullet.CheckCollision(collision_object))
                        {
                            //__Bullet = new Bullet(new Random().Next(Height));
                            bullets_to_remove.Add(bullet);// __Bullets.Remove(bullet);
                            //__GameObjects[i] = new Asteroid(new Point(rand.Next(Width, 2 * Width), rand.Next(0, Height)),
                            //                    new Point(-rand.Next(asteroid_min_speed, asteroid_max_speed), 0),
                            //                    asteroid_size);
                            __GameObjects[i] = null;
                            //MessageBox.Show("Астероид уничтожен!", "Столкновение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    foreach (var bullet in bullets_to_remove)
                    {
                        __Bullets.Remove(bullet);
                    }
                }
            }
        }
    }
}

