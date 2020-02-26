using AsteroidGame.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame.VisualObjects
{
    public abstract class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;

        public Point Position => _Position;

        public Rectangle Rect => new Rectangle(_Position, _Size);

        protected VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            try
            {
                _Size = Size;
//                if (_Size.Width > Game.max_size || _Size.Height > Game.max_size)
//                    throw new VisualObjectSizeException();
            }
            catch(VisualObjectSizeException)
            {
                MessageBox.Show("Перехвачено: Размер объекта слишком большой!", "Создание объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public abstract void Draw(Graphics g);

        public abstract void Update();
    }
}
