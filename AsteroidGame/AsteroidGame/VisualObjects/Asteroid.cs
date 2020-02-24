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
        public Asteroid(Point Position, Point Direction, int ImageSize) : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.asteroid)
        {
        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect)

        public override void Update()
        {
            Random rand = new Random();

            _Position.X += _Direction.X;
            if (_Position.X < 0)
            {
                _Position.X = Game.Width + _Size.Width;
                _Position.Y = rand.Next(0, Game.Height);
                //_Direction.X = rand.Next(-Game.star_max_speed, -Game.star_min_speed);
            }
        }

    }
}
