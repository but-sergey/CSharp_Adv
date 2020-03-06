using AsteroidGame.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    public abstract class CollistionObject : VisualObject, ICollision
    {
        protected CollistionObject(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {
        }

        public bool CheckCollision(ICollision obj)
        {
            var is_collision = Rect.IntersectsWith(obj.Rect);
            return is_collision;
        }
    }
}
