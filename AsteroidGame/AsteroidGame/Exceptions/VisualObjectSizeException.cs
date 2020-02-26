using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame.Exceptions
{
    class VisualObjectSizeException : Exception
    {
        public VisualObjectSizeException()
        {
            MessageBox.Show("Размер объекта слишком большой!", "Создание объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
