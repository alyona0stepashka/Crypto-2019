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
            Console.WriteLine("-------|NOD(21,3) = " + Class1.GetNOD(21, 3)+"|");
            Console.WriteLine("-------|Reverse by n (a=8,n=13) = " + Class1.GetReverse(8, 13) + "|");
            Console.ReadKey();
        }
    }
}
