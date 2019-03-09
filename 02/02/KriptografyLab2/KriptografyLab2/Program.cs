using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptografyLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            String text="";
            Alphabet alphabet = new Alphabet();
            using (StreamReader sr = new StreamReader("D:/alf.txt"))
            {
                text=(sr.ReadToEnd());
                text = text.Replace(" ","");
                Console.WriteLine(text);
            }
            String text2 = "";
            Alphabet alphabet2 = new Alphabet();
            using (StreamReader sr = new StreamReader("D:/alf2.txt"))
            {
                text2 = (sr.ReadToEnd());
                text2 = text.Replace(" ", "");
                Console.WriteLine(text2);
            }
            Console.WriteLine("Entropy: " + alphabet.Entropy(text, alphabet.Cyrillic));
            Console.WriteLine("Entropy: " + alphabet.Entropy(text, alphabet.Latin));
            alphabet.BuildExel(alphabet.Latin);
            Console.WriteLine("Entropy: " + alphabet.Entropy(text, alphabet.Binary));
             
            Console.WriteLine("Infor: " + alphabet.kolInf(alphabet2.Entropy(text2, alphabet2.Latin), text2.Length));

            using (StreamWriter sw = new StreamWriter("D:/byt.txt", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(alphabet.getBytes(alphabet.GetASCII("pakholkoalyonastepanovna")));
            }

            using (StreamReader sr = new StreamReader("D:/byt.txt"))
            {
                text = (sr.ReadToEnd());
                text = text.Replace(" ", "");
            }

            Console.WriteLine("Infor: " + alphabet.kolInf(alphabet.Entropy(text, alphabet.Binary), text.Length));
            Console.WriteLine("Infor: ERROR 0.1 "+alphabet.EfectEntropy(0.1));
            Console.WriteLine("Infor: ERROR 0.5 " + alphabet.EfectEntropy(0.5));
            Console.WriteLine("Infor: ERROR 1 " + alphabet.EfectEntropy(1));
            Console.ReadKey();
        }
    }
}
