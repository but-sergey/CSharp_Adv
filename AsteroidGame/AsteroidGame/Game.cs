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
        
        private const int frame_timeout = 25;

        private static bool move_up = false;
        private static bool move_down = false;
        private static bool shoot = false;

        public static Random rand = new Random();

        public const int moon_height = -100;
        public const int moon_min_size = 200;
        public const int moon_max_size = 900;
        public const int moon_speed = 2;
        
        public const int star_min_size = 3;
        public const int star_max_size = 10;
        public const int star_min_speed = 10;
        public const int star_max_speed = 20;
        public const int star_count = 25;
        public static Pen star_pen = Pens.DarkGray;
        public static Brush star_brush = Brushes.DarkGray;

        public const int asteroid_size = 60;
        public const int asteroid_min_speed = 3;
        public const int asteroid_max_speed = 6;
        public const int clear_zone = 200;           // зона перед кораблем, в которой не генерируются астероиды
        public static int asteroid_count = 10;

        public const int bullet_speed = 10;

        public const int ship_start_energy = 50;
        public const int ship_height = 30;
        public const int ship_width = 60;

        public const int aidkit_energy = 10;
        public const int aidkit_height = 40;
        public const int aidkit_width = 40;
        public const int aidkit_min_speed = 3;
        public const int aidkit_max_speed = 7;

        private static SpaceShip __Ship;
        private static AidKit __AidKit;
        private static VisualObject[] __GameObjects;
        private static List<Asteroid> __Asteroids = new List<Asteroid>();
        private static List<Bullet> __Bullets = new List<Bullet>();
        private static Back[] __Background = new Back[2];

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static int __Score = 0;
        public static int Score { get => __Score; private set => __Score = value; }

        private static Timer timer = new Timer();

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            var g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            timer.Interval = frame_timeout;
            timer.Tick += OnTimerTick;

            form.KeyDown += OnFormKeyDown;
            form.KeyUp += Form_KeyUp;
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:   shoot = false; break;
                case Keys.Up:           move_up = false; break;
                case Keys.Down:         move_down = false; break;
            }
        }

        private static void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.ControlKey:   shoot = true; break;
                case Keys.Up:           move_up = true; break;
                case Keys.Down:         move_down = true; break;
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
            var asteroids = new List<Asteroid>();

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
                __Asteroids.Add(new Asteroid(new Point(rand.Next(clear_zone, Width), rand.Next(0, Height - asteroid_size)),
                                            new Point(-rand.Next(asteroid_min_speed, asteroid_max_speed), 0),
                                            asteroid_size));
            }

            __GameObjects = game_objects.ToArray();
            //__Asteroids = asteroids.ToArray();

            __Bullets.Clear();
            __Ship = new SpaceShip(new Point(10, 400), new Point(5, 5), new Size(ship_width, ship_height));

            __AidKit = null;

            __Ship.ShipEnergyDec += OnShipEnergyDec_LogConsole;
            __Ship.ShipEnergyDec += OnShipEnergyDec_LogFile;

            __Ship.ShipEnergyInc += OnShipEnergyInc_LogConsole;
            __Ship.ShipEnergyInc += OnShipEnergyInc_LogFile;

            __Ship.ShipDestroyed += OnShipDestroyed;
            __Ship.ShipDestroyed += OnShipDestroyed_LogConsole;
            __Ship.ShipDestroyed += OnShipDestroyed_LogFile;

            timer.Start();
        }

        private static void OnShipEnergyInc_LogConsole(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} Космический корабль поймал аптечку! Energy = {__Ship.Energy}");
        }

        private static void OnShipEnergyDec_LogConsole(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} Космический корабль столкнулся с астероидом! Energy = {__Ship.Energy}");
        }

        private static void OnShipDestroyed_LogConsole(object Sender, EventArgs E)
        {
            Console.WriteLine($"{DateTime.Now} Космический корабль уничтожен!");
        }

        private static void OnShipEnergyInc_LogFile(object sender, EventArgs e)
        {
            File.AppendAllText(log_name, $"{DateTime.Now} Космический корабль поймал аптечку! Energy = {__Ship.Energy}");
        }

        private static void OnShipEnergyDec_LogFile(object sender, EventArgs e)
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

            for(var i=0;i<__Background.Length;i++)
                __Background[i].Draw(g);
            
            foreach (var visual_object in __GameObjects)
                visual_object?.Draw(g);

            foreach (var asteroid in __Asteroids)
                asteroid?.Draw(g);

            __Ship.Draw(g);
            __AidKit?.Draw(g);

            foreach(var bullet in __Bullets)
                bullet.Draw(g);

            g.DrawString($"Energy: {__Ship.Energy}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.Orange, 10, 10);
            g.DrawString($"Score: {Score}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.Green, Width - 150, 10);
            g.DrawString($"Asteroids: {__Asteroids.Count}", new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic), Brushes.AntiqueWhite, 400, 10);

            __Buffer.Render();
        }

        public static void Update()
        {
            // обработка команд
            if(shoot)
                __Bullets.Add(new Bullet(new Point(50, __Ship.Rect.Y + __Ship.Rect.Height / 2 - 1)));
            if(move_up)
                __Ship.MoveUp();
            if(move_down)
                __Ship.MoveDown();

            // основной блок
            for (var i = 0; i < __Background.Length; i++)
                __Background[i].Update();

            foreach (var visual_object in __GameObjects)
                visual_object?.Update();

            foreach (var asteroid in __Asteroids)
                asteroid?.Update();

            foreach(var bullet in __Bullets)
                bullet.Update();

            if(__AidKit == null && __Ship.Energy < ship_start_energy)
            {
                __AidKit = new AidKit(new Point(Game.Width + Game.rand.Next(0, 500), Game.rand.Next(0, Game.Height)),
                            new Point(-rand.Next(aidkit_min_speed, aidkit_max_speed), 0),
                            new Size(aidkit_width, aidkit_height));
            }

            __AidKit?.Update();

            if(__AidKit != null && __Ship.CheckCollision(__AidKit))
            {
                //__Ship.ChangeEnergy(__AidKit.Power);
                __AidKit = null;
            }

            var asteroid_to_renew_count = 0;
            var asteroid_to_remove = new List<Asteroid>();
            var bullets_to_remove = new List<Bullet>();

            foreach (var asteroid in __Asteroids)
            //for (var i = 0; i < __Asteroids.Length; i++)
            {
                if (__Ship.CheckCollision(asteroid))
                {
                    asteroid_to_remove.Add(asteroid);
                    asteroid_to_renew_count++;
                }

                foreach (var bullet in __Bullets)
                    if (bullet.CheckCollision(asteroid))
                    {
                        Score += 2 * asteroid.Power;

                        bullets_to_remove.Add(bullet);
                        asteroid_to_remove.Add(asteroid);
                    }
            }

            // удаление сбитых астероидов
            foreach (var asteroid in asteroid_to_remove)
            {
                __Asteroids.Remove(asteroid);
            }
            // удаление попавших пуль
            foreach (var bullet in bullets_to_remove)
            {
                __Bullets.Remove(bullet);
            }
            // если астероиды закончились - то накинуть ))
            if(__Asteroids.Count + asteroid_to_renew_count == 0)
            {
                asteroid_count += 5;
                asteroid_to_renew_count = asteroid_count;
            }
            // генерация астероидов
            for (var i = 0; i < asteroid_to_renew_count; i++)
            {
                __Asteroids.Add(new Asteroid(new Point(rand.Next(clear_zone, Width), rand.Next(0, Height - asteroid_size)),
                                    new Point(-rand.Next(asteroid_min_speed, asteroid_max_speed), 0),
                                    asteroid_size));
            }
        }
    }
}

