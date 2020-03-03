using System;
using System.Security.Cryptography;

namespace SymetricKeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var cry = new RSACng(512);
            var key = Base64Encode(cry.ToXmlString(true));

            Console.WriteLine(key);
            Console.ReadKey();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
