using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Classes
{
    public abstract class BaseWorker
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        protected BaseWorker(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public abstract double CalcSalary();
    }
}
