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
        //private static Image _Star = Properties.Resources.star;
        private static Pen _star_pen;

        public Star(Point Position, Point Direction, int StarSize, Pen StarPen)
            : base(Position, Direction, new Size(StarSize, StarSize))//, Properties.Resources.star)
        {
            _star_pen = StarPen;
        }

        public override void Draw(Graphics g)
        {
            //g.DrawImage(_Star,
            //    _Position.X, _Position.Y,
            //    _Size.Width, _Size.Height);
            var p1 = new Point((int)(Position.X + 0.15 * _Size.Width), (int)(Position.Y + 0.15 * _Size.Height));
            var p2 = new Point((int)(Position.X + 0.85 * _Size.Width), (int)(Position.Y + 0.85 * _Size.Height));
            
            var p3 = new Point((int)(Position.X + 0.15 * _Size.Width), (int)(Position.Y + 0.85 * _Size.Height));
            var p4 = new Point((int)(Position.X + 0.85 * _Size.Width), (int)(Position.Y + 0.15 * _Size.Height));

            var p5 = new Point(Position.X + _Size.Width / 2, Position.Y);
            var p6 = new Point(Position.X + _Size.Width / 2, Position.Y + _Size.Height);

            var p7 = new Point(Position.X, Position.Y + _Size.Height / 2);
            var p8 = new Point(Position.X + _Size.Width, Position.Y + _Size.Height / 2);
            
            g.DrawLine(_star_pen, p1, p2);
            g.DrawLine(_star_pen, p3, p4);
            g.DrawLine(_star_pen, p5, p6);
            g.DrawLine(_star_pen, p7, p8);
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
