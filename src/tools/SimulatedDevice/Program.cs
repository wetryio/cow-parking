using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SimulatedDevice
{
    class Program
    {
        private static string idScope = Environment.GetEnvironmentVariable("SCOPE_ID"); // "0ne000CEB9C";
        private const string GlobalDeviceEndpoint = "global.azure-devices-provisioning.net";
        private static string enrollmentGroupPrimaryKey = Environment.GetEnvironmentVariable("PRIMARY_KEY"); // "NrEVzOt8iXlv4LlWkFnQms /PPFreOvaHTv84LtHmTvMoSN/GFKiKu3fV7ZjWpuZpBRgrkWDRvbUKjUiMjUFHAg==";
        private static string enrollmentGroupSecondaryKey = Environment.GetEnvironmentVariable("SECONDARY_KEY"); //"ozi3vfEQ/mLsPVGBAz7Dz3Utne9sR+sshIMaCI5tlpGyaxuwQ1JpRgxiqm4ZBxwy/uY5oz8exztdQA/piHdtrw==";
        private static string registrationId = $"simulateddevice-{Guid.NewGuid()}";

        public static int Main(string[] args)
        {
            if (string.IsNullOrWhiteSpace(idScope) && (args.Length > 0))
            {
                idScope = args[0];
            }

            if (string.IsNullOrWhiteSpace(idScope))
            {
                Console.WriteLine("ProvisioningDeviceClientSymmetricKey <IDScope>");
                return 1;
            }

            string primaryKey = "";
            string secondaryKey = "";
            if (!String.IsNullOrEmpty(registrationId) && !String.IsNullOrEmpty(enrollmentGroupPrimaryKey) && !String.IsNullOrEmpty(enrollmentGroupSecondaryKey))
            {
                primaryKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(enrollmentGroupPrimaryKey), registrationId);
                secondaryKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(enrollmentGroupSecondaryKey), registrationId);
            }
            else
            {
                Console.WriteLine("Invalid configuration provided, must provide group enrollment keys or individual enrollment keys");
                return -1;
            }

            using (var security = new SecurityProviderSymmetricKey(registrationId, primaryKey, secondaryKey))

            using (var transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.TcpOnly))
            {
                ProvisioningDeviceClient provClient =
                    ProvisioningDeviceClient.Create(GlobalDeviceEndpoint, idScope, security, transport);

                var sample = new ProvisioningDevice(provClient, security);
                sample.RunSampleAsync().GetAwaiter().GetResult();
            }
            Console.WriteLine("Enter any key to exit");
            Console.ReadLine();
            return 0;
        }

        /// <summary>
        /// Generate the derived symmetric key for the provisioned device from the enrollment group symmetric key used in attestation
        /// </summary>
        /// <param name="masterKey">Symmetric key enrollment group primary/secondary key value</param>
        /// <param name="registrationId">the registration id to create</param>
        /// <returns>the primary/secondary key for the member of the enrollment group</returns>
        public static string ComputeDerivedSymmetricKey(byte[] masterKey, string registrationId)
        {
            using (var hmac = new HMACSHA256(masterKey))
            {
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationId)));
            }
        }
    }
}
