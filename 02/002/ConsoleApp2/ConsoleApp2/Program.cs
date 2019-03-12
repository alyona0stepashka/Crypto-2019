using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        static void Main(string[] args)
        {
            string alphabet = "qwertyuiopasdfghjklzxcvbnm";
            using (StreamReader sr = new StreamReader("E:\\Настя\\6sem\\КМЗИ\\ConsoleApp2\\ConsoleApp2\\TextFile2.txt"))
            {
                Console.WriteLine("Entropia Shannon  " + Shannon(GetText("E:\\Настя\\6sem\\КМЗИ\\ConsoleApp2\\ConsoleApp2\\TextFile2.txt"), alphabet));
                Console.WriteLine("Kolichestvo informatsii    " + KolvoInfoShannon(sr.ReadToEnd(), Shannon(GetText("E:\\Настя\\6sem\\КМЗИ\\ConsoleApp2\\ConsoleApp2\\TextFile2.txt"), alphabet)));
            }
            using (StreamReader sr = new StreamReader("E:\\Настя\\6sem\\КМЗИ\\ConsoleApp2\\ConsoleApp2\\TextFile2.txt"))
            {
                Console.WriteLine("Entropia Hartley  " + Hartley(alphabet));
                Console.WriteLine("Kolichestvo informatsii    " + KolvoInfoHartley(sr.ReadToEnd(), Hartley(alphabet)));
            }
            string alphabet1 = "10";

                Console.WriteLine(ToBinAscii("ShaveikoAnastasiaAlekseevna"));
                Console.WriteLine("Entropia Shannon  " + Shannon(ToBinAscii("ShaveikoAnastasiaAlekseevna"), alphabet1));
                Console.WriteLine("Kolichestvo informatsii    " + KolvoInfoShannon(ToBinAscii("ShaveikoAnastasiaAlekseevna"), Shannon(ToBinAscii("ShaveikoAnastasiaAlekseevna"), alphabet1)));

            Console.WriteLine(" 0.1 " + EfectiveEntropy(0.1));
            Console.WriteLine(" 0.5 " + EfectiveEntropy(0.5));
            Console.WriteLine(" 1 " + EfectiveEntropy(1));
        }

        static string GetText(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string str;
                    str = Regex.Replace(sr.ReadToEnd(), "[^a-zA-Z]", "");
                    return str;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        static double Shannon(string text, string alphabet)
        {
            double shn = 0;
            for(int i = 0; i < alphabet.Length; i++)
            {
                int count = Regex.Matches(text, alphabet[i].ToString(), RegexOptions.IgnoreCase).Count;
                double p = (count == 0) ? 0 : (double)count / text.Length;
                if (p != 0)
                    shn += p * Math.Log(p);
                Console.WriteLine(alphabet[i].ToString() + "     " + p);
            }
            return -shn;
        }

        static double KolvoInfoShannon(string text, double shn)
        {
            return text.Length * shn;
        }

        static double Hartley(string alphabet)
        {
            return Math.Log(alphabet.Length);
        }

        static double KolvoInfoHartley(string text, double hrtl)
        {
            return text.Length * hrtl;
        }

        static string ToBinAscii(string text)
        {
            string str = "";
            byte[] arr = Encoding.ASCII.GetBytes(text);
            int[] b = arr.Select(i => (int)i).ToArray();
            foreach (int i in b)
                str += Convert.ToString(i, 2);
            return str;
        }

       static double EfectiveEntropy(double error)
        {
            return 1 - (-error * Math.Log(error) - (1 - error) * Math.Log((1 - error)));
        }
    }
}
