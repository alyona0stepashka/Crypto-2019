using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using GOST;

namespace Lab7
{
    internal class Program
    {
        /*
        Симметричная криптография -2

        1. Разработать программную реализацию шифрования алгоритмом Lucifer.
        http://crypto.pp.ua/2010/12/algoritm-lucifer/
        2. Разработать программную реализацию шифрования алгоритмом Blowfish.
        https://ru.wikipedia.org/wiki/Blowfish
        3. Разработать программную реализацию шифрования алгоритмом ГОСТ 28147-89.
        http://www.vr-online.ru/blog/gost-28147-89-na-c-4968
        4. Сравнить скорость шифрования для всех симметричных алгоритмов.
         */
        private static void Main()
        {
            var sw = new Stopwatch();
            Console.WriteLine("Enter crypted string");
            var str = "Hello world!!\tHello world!!\tHello world!!\tHello world!!\t"; // Console.ReadLine();
            Console.WriteLine("Start");
            Console.WriteLine("Lucifer");
            sw.Reset();
            sw.Start();
            Console.WriteLine(sw.ElapsedMilliseconds);
            var luciferCrypted = Lucifer.EnCrypt(str);
            sw.Stop();
            Console.WriteLine("Time crypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("crypted = "       + luciferCrypted);
            sw.Reset();
            sw.Start();
            var luciferDecrypted = Lucifer.DeCrypt(luciferCrypted);
            sw.Stop();
            Console.WriteLine("Time decrypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("decrypted = "       + luciferDecrypted);

            Console.WriteLine("Blowfish");
            sw.Reset();
            sw.Start();
            Console.WriteLine(sw.ElapsedMilliseconds);
            var blowFishCrypted = BlowFishClass.EnCrypt(str);
            sw.Stop();
            Console.WriteLine("Time crypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("crypted = "       + blowFishCrypted);
            sw.Reset();
            sw.Start();
            var blowFishDecrypted = BlowFishClass.DeCrypt(blowFishCrypted);
            sw.Stop();
            Console.WriteLine("Time decrypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("decrypted = "       + blowFishDecrypted);


            Console.WriteLine("GOST");
            sw.Reset();
            sw.Start();
            Console.WriteLine(sw.ElapsedMilliseconds);
            var gostCrypted = Gost.EnCrypt(str);
            sw.Stop();
            Console.WriteLine("Time crypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("crypted = "       + gostCrypted);
            sw.Reset();
            sw.Start();
            var gostDecrypted = Gost.DeCrypt(gostCrypted);
            sw.Stop();
            Console.WriteLine("Time decrypting = " + sw.ElapsedMilliseconds);
            Console.WriteLine("decrypted = "       + gostDecrypted);


            Console.ReadLine();
        }
    }

    public static class Lucifer
    {
        private static readonly byte[] Bytes = Encoding.ASCII.GetBytes("SSECRETT");

        public static string EnCrypt(string str)
        {
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(Bytes, Bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(str);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
        }

        public static string DeCrypt(string cryptedString)
        {
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(Bytes, Bytes),
                CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }

    public static class Gost
    {
        public static string EnCrypt(string str)
        {
            var bytes = Encoding.Default.GetBytes(str);
            var key = Encoding.Default.GetBytes("12345678901234567890123456789012");
            var iv = Encoding.Default.GetBytes("12345678");

            var gost = new GOSTManaged();
            var encoded = gost.XOREncode(key, iv, bytes);
            gost.Dispose();
            return Encoding.Default.GetString(encoded);
        }

        public static string DeCrypt(string str)
        {
            var bytes = Encoding.Default.GetBytes(str);
            var key = Encoding.Default.GetBytes("12345678901234567890123456789012");
            var iv = Encoding.Default.GetBytes("12345678");

            var gost = new GOSTManaged();
            var encoded = gost.XORDecode(key, iv, bytes);
            gost.Dispose();
            return Encoding.Default.GetString(encoded);
        }
    }

    public static class BlowFishClass
    {
        private static readonly BlowFish Crypter = new BlowFish("secret key");

        public static string EnCrypt(string str)
        {
            var result = Crypter.Encrypt_CTR(str);
            return result;
        }

        public static string DeCrypt(string str)
        {
            var result = Crypter.Decrypt_CTR(str);
            return result;
        }
    }
}