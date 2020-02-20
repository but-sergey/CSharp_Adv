using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            Gamer gamer = new Gamer("Sergey", new DateTime(1985, 06, 05, 11, 30, 00));

            Gamer[] gamers = new Gamer[100];
            for(var i = 0; i < gamers.Length; i++)
            {
                var g = new Gamer($"Gamer {i + 1}", DateTime.Now.Subtract(TimeSpan.FromDays(365 * (i + 18))));
                gamers[i] = g;
            }
            
            gamer.SayYouName();

            Console.WriteLine();

            foreach (var g in gamers)
                g.SayYouName();

            Console.WriteLine();

            //gamer.SetName("2222");
            //Console.WriteLine("Gamer's name is {0}", gamer.GetName());

            gamer.Name = "123";

            Console.WriteLine("Gamer's name is {0}", gamer.Name);

            Console.ReadLine();
        }
    }

}
