using AsteroidGame.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    public class Asteroid : ImageObject, ICollision
    {
        public int Power { get; set; } = 3;

        public Asteroid(Point Position, Point Direction, int ImageSize) : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.asteroid)
        {
        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);

        public override void Update()
        {
            _Position = new Point(_Position.X + _Direction.X, _Position.Y);
            if (_Position.X < -_Size.Width)
            {
                _Position = new Point(Game.Width + _Size.Width, Game.rand.Next(0, Game.Height));
            }
        }

    }
}
