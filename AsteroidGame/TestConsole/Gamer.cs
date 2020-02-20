using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{

    class Gamer
    {
        private string _Name;
        private DateTime _DayOfBirth;

        public string Name
        {
            get
            {
                return _Name ?? String.Empty;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _Name = value;
            }
        }

        public Gamer(string Name, DateTime DayOfBirth)
        {
            _Name = Name;
            _DayOfBirth = DayOfBirth;
        }

        //public void SetName(string value)
        //{
        //    _Name = value;
        //}

        //public string GetName()
        //{
        //    return _Name;
        //}

        internal int GetNameLength()
        {
            return Name.Length;
        }

        public void SayYouName()
        {
            Console.WriteLine("My name is {0} - {1:dd:MM:yyyy HH:mm:ss}", _Name, _DayOfBirth);
        }
    }
}
