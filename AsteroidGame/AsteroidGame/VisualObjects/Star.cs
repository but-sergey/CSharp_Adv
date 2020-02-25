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

        public Star(Point Position, Point Direction, int StarSize)
            : base(Position, Direction, new Size(StarSize, StarSize))
        {
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(_Star,
                _Position.X, _Position.Y,
                _Size.Width, _Size.Height);
            //var p1 = Position;
            //var p2 = new Point(p1.X + _Size.Width, p1.Y + _Size.Height);
            //var p3 = new Point(p1.X, p1.Y + _Size.Height);
            //var p4 = new Point(p1.X + _Size.Width, p1.Y);
            //g.DrawLine(Pens.Gray, p1, p2);
            //g.DrawLine(Pens.Gray, p3, p4);
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
