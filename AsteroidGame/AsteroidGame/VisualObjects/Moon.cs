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
            Random rand = new Random();

            _Position.X += _Direction.X;
            if (_Position.X < -_Size.Width)
            {
                _Position.X = Game.Width;
                _Position.Y = rand.Next(-100, Game.Height - 100);
                _Size.Height = rand.Next(200, 500);
                _Size.Width = _Size.Height;
            }

        }
    }
}
