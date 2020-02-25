using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Classes
{
    public class HoursRateWorker : BaseWorker, IComparable
    {
        public HoursRateWorker(string name, DateTime birth_date, double hours_rate) 
            : base(name, birth_date)
        {
            HoursRate = hours_rate;
        }

        public double HoursRate { get; set; }

        public override double CalcSalary()
        {
            return 20.8 * 8 * HoursRate;
        }

        public int CompareTo(object obj)
        {
            if (obj is BaseWorker)
            {
                if (CalcSalary() == ((HoursRateWorker)obj).CalcSalary())
                    return 0;
                else if (CalcSalary() > ((HoursRateWorker)obj).CalcSalary())
                    return +1;
                else
                    return -1;
            }
            else if (obj is null)
                throw new ArgumentNullException("Попытка сравнения с пустотой");
            else
                throw new ArgumentException("Попытка сравнения с " + obj.GetType().Name, nameof(obj));
        }
    }
}
