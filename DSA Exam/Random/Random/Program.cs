using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randoma
{
    class Programa
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var output = rand.Next(0, 3);
            Console.WriteLine(output);
        }
    }
}
