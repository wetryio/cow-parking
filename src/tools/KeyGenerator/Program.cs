using System;
using System.Security.Cryptography;
using System.Text;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create unique device key");

            string masterKey = "XMbXhPNmLY0INTr4Be2B8hK6Flj0aWsLug9kxysc00rHx5FMCCJghJVuYxmf+X0XewkuAnwLtX0xDpgnJ0oZDA==";

            string registrationId = "device02"; // unique device

            String deviceKey =
                Utils.ComputeDerivedSymmetricKey(
                           Convert.FromBase64String(masterKey),
                           registrationId);

            Console.WriteLine(
    $"device key for registration {registrationId} =\n{deviceKey}");

            Console.WriteLine("\nPress a key to exit");

            Console.ReadKey();
        }
    }

    public static class Utils
    {
        public static string ComputeDerivedSymmetricKey(
                        byte[] masterKey, string registrationId)
        {
            using (var hmac = new HMACSHA256(masterKey))
            {
                return Convert.ToBase64String(
                         hmac.ComputeHash(
                           Encoding.UTF8.GetBytes(
                             registrationId)));
            }
        }
    }
}
