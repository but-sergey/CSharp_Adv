using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidGame.VisualObjects.Interfaces;

namespace AsteroidGame.VisualObjects
{
//    public class SpaceShip : VisualObject, ICollision
    public class SpaceShip : ImageObject, ICollision
    {
        public event EventHandler ShipDestroyed;
        public event EventHandler ShipCollisioned;

        private int _Energey = 5;

        public int Energy => _Energey;

        public SpaceShip(Point Position, Point Direction, Size ShipSize)
            : base(Position, Direction, ShipSize, Properties.Resources.ship)
        {
        }

        //public override void Draw(Graphics g)
        //{
        //    var rect = Rect;
        //    g.FillEllipse(Brushes.Blue, rect);
        //    g.DrawEllipse(Pens.Yellow, rect);

        //}

        public override void Update()
        {
        }

        public void ChangeEnergy(int delta)
        {
            _Energey += delta;
            if (_Energey <= 0)
                ShipDestroyed?.Invoke(this, EventArgs.Empty);
        }

        public void MoveUp()
        {
            if (_Position.Y > 0)
                _Position = new Point(_Position.X, _Position.Y - _Direction.Y);
        }

        public void MoveDown()
        {
            if (_Position.Y - _Size.Height < Game.Height)
                _Position = new Point(_Position.X, _Position.Y + _Direction.Y);
        }

        public bool CheckCollision(ICollision obj)
        {
            var is_collision = Rect.IntersectsWith(obj.Rect);
            if (is_collision && obj is Asteroid asteroid)
            {
                //obj = null;
                ChangeEnergy(-asteroid.Power);

                if(_Energey > 0)
                    ShipCollisioned.Invoke(this, EventArgs.Empty);
            }
            return is_collision;

        }
    }
}
