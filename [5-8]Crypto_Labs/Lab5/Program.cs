using System;
using System.Collections.Generic;



namespace Lab5
{
    /*
        1. Разработать программную реализацию вычисления НОД методом Евклида.
        2. Разработать программную реализацию вычисления обратного значение по модулю.
        Вопросы.
        1. Поясните принцип вычисления НОД методом Евклида.
         2. Поясните принцип вычисления обратного значения по модулю.
         3. Сформулируйте и поясните китайскую теорему об остатках.
         В общем случае, если разложение числа n на простые множители представляет собой p1 ⋅ p2 ⋅ … ⋅ pt (некоторые простые числа могут встречаться несколько раз), то система уравнений
         x mod pi = ai, где i = 1, 2, …, t  имеет единственное решение: x, меньшее n.
         4. Сформулируйте и поясните малую теорему Ферма.
         Если n – простое число, а число а не кратно n, то справедливо
           a^n= 1 mod n
        y=a^x mod n

   */

    internal class Program
    {
        private static void Main()
        {
            var arr1 = new List<int> { 1, 2, 3, 169, 13, 5 };
            var arr2 = new List<int> {1, 2, 3, 169, 13, 5 };
            foreach (var t1 in arr1)
            {
                foreach (var t2 in arr2)
                    Console.WriteLine($"НОД {t1} и {t2} : {Gcd(t1, t2)}");
            }
            Console.WriteLine(Gcd(7,5));
            //inverse modulo
            Console.WriteLine("inverse modul");
            Console.WriteLine(BinModPow(5, 7, 13));
            Console.WriteLine(BinModPow(7, 1, 20));
            //var x1 = 0;
            //var y1 = 0;
            //Console.WriteLine("Gcd");
            //Console.WriteLine(Gcd(7, 13, ref x1, ref y1));
            //Console.WriteLine(x1 + " Gcd x"); // 7 * 2(x1) + 13 * (-1)(н1) = 1
            //Console.WriteLine(y1 + " Gcd y");
            //var x2 = 0;
            //var y2 = 0;
            //Console.WriteLine("Gcd");
            //Console.WriteLine(Gcd(7, 20, ref x2, ref y2));
            //Console.WriteLine(x2 + " Gcd x"); // 7 * 3(x2) + 20 * (-1) (y2) = 1
            //Console.WriteLine(y2 + " Gcd y");
            //Console.ReadLine();
        }

        public static int Gcd(int a, int b)
        {
            if (a == 1 || b == 1)
            {
                return 1;
            }

            if (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a = a % b;
                }
                else
                {
                    b = b % a;
                }

                return Gcd(a, b);
            }

            return a + b;
        }

        // в лекции:
        // xy + kn = 1
        // y ^ (-1) = x mod n
        // x ^ (-1) = y mod n
        //     x = 7, y = -1, k = 13, n = 2
        // 7 * 3 + 20 * (-1) = 1
        private static int Gcd(int a, int b, ref int x, ref int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int x1 = 0, y1 = 0;
            var d = Gcd(b % a, a, ref x1, ref y1);
            x = y1 - b / a * x1;
            y = x1;
            return d;
        }

        public static double BinModPow(double a, double b, double n)
        {
            var x = (decimal) b;
            var i = 0;
            do
            {
                if (x % 2 == 1)
                {
                    x--;
                }

                x = x / 2;
                i++;
            } while (x > 0);

            x = (decimal) b;
            var ar = new decimal[i];
            for (i = 0; i < ar.Length; i++)
            {
                ar[i] = x % 2;
                if (x % 2 == 1)
                {
                    x--;
                }

                x = x / 2;
            }

            x = (decimal) a;
            for (i = ar.Length - 2; i >= 0; i--)
            {
                x = x * x  % (decimal) n;
                x = x * (decimal) Math.Pow(a, (double) ar[i]) % (decimal) n;
            }

            return (double) x;
        }
    }
}