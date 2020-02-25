using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Classes
{
    public class HoursRateWorker : BaseWorker
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

    }
}
