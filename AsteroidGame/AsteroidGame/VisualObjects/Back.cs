using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Back : ImageObject
    {
        public Back(Point Position, Point Direction, Size ImageSize)
            : base(Position, Direction, ImageSize, Properties.Resources.background)
        {
        }

        public override void Update()
        {
            _Position = new Point(_Position.X + _Direction.X, _Position.Y);

            if (Rect.Right < 0) _Position = new Point(Rect.Right + Rect.Width, _Position.Y);

        }
    }
}
