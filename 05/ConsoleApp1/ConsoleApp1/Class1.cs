using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   public class Class1
    {
        public static int GetNOD(int val1, int val2)
        {
            val1 = Math.Abs(val1);
            val2 = Math.Abs(val2);
            if (val2 == 0)
                return val1;
            else
                return GetNOD(val2, val1 % val2);
        }

        public static double BinModPow(double a, double b, double n)
        {
            decimal x = (decimal)b;
            int i = 0;
            do
            {
                if (x % 2 == 1) x--;
                x = x / 2;
                i++;
            }
            while (x > 0);
            x = (decimal)b;
            decimal[] ar = new decimal[i];
            for (i = 0; i < ar.Length; i++)
            {
                ar[i] = x % 2;
                if (x % 2 == 1) x--;
                x = x / 2;
            }
            x = (decimal)a;
            for (i = ar.Length - 2; i >= 0; i--)
            {
                x = (x * x) % (decimal)n;
                x = (x * (decimal)Math.Pow(a, (double)ar[i])) % (decimal)n;
            }
            return (double)x;
        }
    }
}
