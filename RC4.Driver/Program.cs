using System;
using System.Collections.Generic;
using System.Text;

namespace RC4.Driver
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            byte[] key = { 1, 2, 3, 4, 5 };

            var dataText = @"Hello Greg!";
            var data = Encoding.Unicode.GetBytes(dataText);

            var cipher = RC4.Encrypt(key, data);
            var cipherText = Encoding.Unicode.GetString(cipher);

            var retrieved = RC4.Decrypt(key, cipher);
            var retrievedText = Encoding.Unicode.GetString(retrieved);

            Console.WriteLine($"Data text     : {dataText}");
            Console.WriteLine($"Cipher text   : {cipherText}");
            Console.WriteLine($"Cipher bytes   : {PrintByteArray(cipher)}");
            Console.WriteLine($"Retrieved text: {retrievedText}");

            Console.WriteLine($"Period of first pseudo-random byte: {RC4.GetKeyInitializationPeriod(key, 1)}");
            Console.WriteLine($"Period of second pseudo-random byte: {RC4.GetKeyInitializationPeriod(key, 2)}");

            Console.ReadKey();
        }
        public static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" }");
            return sb.ToString();
        }
    }

}
