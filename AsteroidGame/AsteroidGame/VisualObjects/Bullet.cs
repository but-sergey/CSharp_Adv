﻿using AsteroidGame.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    public class Bullet : CollistionObject
    {
        private const int __BulletSizeX = 20;
        private const int __BulletSizeY = 5;

        public Bullet(Point Position) : base(Position, Point.Empty, new Size(__BulletSizeX, __BulletSizeY))
        {
        }

        public override void Draw(Graphics g)
        {
            var rect = Rect;
            g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }

        public override void Update()
        {
            _Position = new Point(_Position.X + Game.bullet_speed, _Position.Y);
        }
    }
}
