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
            _Position = new Point(_Position.X +_Direction.X, Position.Y);
            if (_Position.X < -_Size.Width)
            {
                _Position = new Point(Game.Width, Game.rand.Next(-100, Game.Height - 100));
                int TempSize = Game.rand.Next(Game.moon_min_size, Game.moon_max_size);
                _Size = new Size(TempSize, TempSize);
            }

        }
    }
}
