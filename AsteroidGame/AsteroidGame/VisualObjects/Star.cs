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
        public Star(Point Position, Point Direction, int StarSize)
            : base(Position, Direction, new Size(StarSize, StarSize))
        {
        }

        public override void Draw(Graphics g)
        {
            var p1 = new Point((int)(_Position.X - 0.4 * _Size.Width), (int)(_Position.Y - 0.4 * _Size.Height));
            var p2 = new Point((int)(_Position.X + 0.4 * _Size.Width), (int)(_Position.Y + 0.4 * _Size.Height));
            
            var p3 = new Point((int)(_Position.X - 0.4 * _Size.Width), (int)(_Position.Y + 0.4 * _Size.Height));
            var p4 = new Point((int)(_Position.X + 0.4 * _Size.Width), (int)(_Position.Y - 0.4 * _Size.Height));

            var p5 = new Point(_Position.X, _Position.Y - _Size.Height / 2);
            var p6 = new Point(_Position.X, _Position.Y + _Size.Height / 2);

            var p7 = new Point(_Position.X - _Size.Width / 2, _Position.Y);
            var p8 = new Point(_Position.X + _Size.Width / 2, _Position.Y);
            
            g.DrawLine(Game.star_pen, p1, p2);
            g.DrawLine(Game.star_pen, p3, p4);
            g.DrawLine(Game.star_pen, p5, p6);
            g.DrawLine(Game.star_pen, p7, p8);
            g.FillEllipse(Game.star_brush,
                        new Rectangle(new Point((int)(_Position.X - 0.3 * _Size.Width), (int)(_Position.Y - 0.3 * _Size.Height)),
                        new Size((int)(0.6 * _Size.Width), (int)(0.6 * _Size.Height))));
        }

        public override void Update()
        {
            _Position = new Point(_Position.X + _Direction.X, _Position.Y);
            if (_Position.X < 0)
            {
                _Position = new Point(Game.Width, Game.rand.Next(0, Game.Height));
                _Direction = new Point(-Game.rand.Next(Game.star_min_speed, Game.star_max_speed), 0);
            }
        }
    }
}
