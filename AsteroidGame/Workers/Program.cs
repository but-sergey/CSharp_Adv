using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Classes;

namespace Workers
{
    class Program
    {
        
        static void Main(string[] args)
        {
            const int WorkersCount = 100;
            BaseWorker[] Workers = new BaseWorker[WorkersCount];

            Random rnd = new Random();

            for(int i = 0; i < WorkersCount; i++)
            {
                if (rnd.Next(100) > 50)
                    Workers[i] = new HoursRateWorker($"Worker {i}", DateTime.Today, rnd.Next(100, 300));
                else
                    Workers[i] = new MonthRateWorker($"Worker {i}", DateTime.Today, rnd.Next(10000, 60000));
            }

            Array.Sort(Workers);

            Console.ReadKey();
        }
    }
}
