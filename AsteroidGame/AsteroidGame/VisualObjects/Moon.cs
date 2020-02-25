using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Moon : ImageObject
    {
        public Moon(Point Position, Point Direction, int ImageSize)
            : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.moon)
        {
        }

        public override void Update()
        {
            _Position.X += _Direction.X;
            if (_Position.X < -_Size.Width)
            {
                _Position.X = Game.Width;
            }

        }
    }
}
