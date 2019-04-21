using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lab6
{
    /*
    Симметричная криптография

    1. Разработать программную реализацию шифрования алгоритмом DES.
    2. Разработать программную реализацию шифрования алгоритмом 3DES.
    
    Вопросы.
    1. Поясните общий принцип алгоритма DES.
    http://www.intuit.ru/studies/courses/691/547/lecture/12377
    2. Перечислите последовательность операций одного раунда алгоритма DES.
    3. В чем заключается модификация алгоритма 3DES?
    https://ru.wikipedia.org/wiki/Triple_DES
     */
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter crypted text");
            var str = Console.ReadLine();
            Console.WriteLine("DES");
            var crypted = Des.Crypt(str);
            Console.WriteLine("crypted = " + crypted);
            var decrypted = Des.Decrypt(crypted);
            Console.WriteLine("decrypted = " + decrypted);
            Console.WriteLine("Triple DES");
            crypted = TripleDes.Crypt(str);
            Console.WriteLine("crypted = " + crypted);
            decrypted = TripleDes.Decrypt(crypted);
            Console.WriteLine("decrypted = " + decrypted);
            Console.ReadLine();
        }
    }

    public class Des
    {
        public static string Crypt(string text)
        {
            var key = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};
            var iv = new byte[] {0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8};

            var bytes = Encoding.Default.GetBytes(text);
            var data = Encrypt(bytes, key, iv);
            return Encoding.Default.GetString(data);
        }

        public static string Decrypt(string text)
        {
            var key = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};
            var iv = new byte[] {0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8};

            var bytes = Encoding.Default.GetBytes(text);
            var data = Decrypt(bytes, key, iv);
            return Encoding.Default.GetString(data);
        }

        public static byte[] Encrypt(byte[] inputBytes, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CFB;
                des.Padding = PaddingMode.Zeros;

                var encryptor = des.CreateEncryptor(key, iv);

                var stream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                }

                return stream.ToArray().Take(inputBytes.Length).ToArray();
            }
        }

        public static byte[] Decrypt(byte[] inputBytes, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CFB;
                des.Padding = PaddingMode.Zeros;

                var decryptor = des.CreateDecryptor(key, iv);
                var input = new List<byte>(inputBytes);
                if (inputBytes.Length % 8 != 0)
                {
                    input.AddRange(new byte[8 - inputBytes.Length % 8]);
                }

                using (var result = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(new MemoryStream(input.ToArray()), decryptor,
                        CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(result);
                    }

                    return result.ToArray().Take(inputBytes.Length).ToArray();
                }
            }
        }
    }

    public class TripleDes
    {
        public static string Crypt(string text)
        {
            var key1 = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};
            var key2 = new byte[] {0x02, 0x03, 0x15, 0x11, 0x13, 0x03, 0x04, 0x06};
            var key3 = new byte[] {0x22, 0x11, 0x12, 0x13, 0x14, 0x9, 0x08, 0x07};
            var iv = new byte[] {0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8};

            var bytes = Encoding.Default.GetBytes(text);
            var data = Des.Encrypt(bytes, key1, iv);
            data = Des.Encrypt(data, key2, iv);
            data = Des.Encrypt(data, key3, iv);
            return Encoding.Default.GetString(data);
        }

        public static string Decrypt(string text)
        {
            var key3 = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};
            var key2 = new byte[] {0x02, 0x03, 0x15, 0x11, 0x13, 0x03, 0x04, 0x06};
            var key1 = new byte[] {0x22, 0x11, 0x12, 0x13, 0x14, 0x9, 0x08, 0x07};
            var iv = new byte[] {0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8};

            var bytes = Encoding.Default.GetBytes(text);
            var data = Des.Decrypt(bytes, key1, iv);
            data = Des.Decrypt(data, key2, iv);
            data = Des.Decrypt(data, key3, iv);
            return Encoding.Default.GetString(data);
        }
    }
}