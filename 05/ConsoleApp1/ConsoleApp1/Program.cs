using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------NOD(21,3) = " + Class1.GetNOD(21, 3));
            Console.WriteLine("-------Reverce by n (a=12,b=8,n=2) = " + Class1.BinModPow(12, 8, 2));
            Console.ReadKey();
        }
    }
}
