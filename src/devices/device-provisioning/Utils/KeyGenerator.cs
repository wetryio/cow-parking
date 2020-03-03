using System;
using System.Security.Cryptography;
using System.Text;

namespace DeviceProvisioning.Utils
{
    public class KeyGenerator
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
