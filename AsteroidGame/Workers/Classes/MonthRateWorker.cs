using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Classes
{
    public class MonthRateWorker : BaseWorker
    {
        public MonthRateWorker(string name, DateTime birth_date, double month_rate)
            : base(name, birth_date)
        {
            MonthRate = month_rate;
        }

        public double MonthRate { get; set; }

        public override double CalcSalary()
        {
            return MonthRate;
        }
    }
}
