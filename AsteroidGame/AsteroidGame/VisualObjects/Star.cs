using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Star : VisualObject
    {
        //private static Image _Star = Image.FromFile("img\\star.jpg");
        private static Image _Star = Properties.Resources.star;

        public Star(Point Position, Point Direction, int Size)
            : base(Position, Direction, new Size(Size, Size))
        {
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(_Star,
                _Position.X, _Position.Y,
                _Size.Width, _Size.Height);
        }

        public override void Update()
        {
            Random rand = new Random();

            _Position.X += _Direction.X;
            if (_Position.X < 0)
            {
                _Position.X = Game.Width + _Size.Width;
                _Position.Y = rand.Next(0, Game.Height);
                _Direction.X = rand.Next(-Game.star_max_speed, -Game.star_min_speed);
            }
        }
    }
}
