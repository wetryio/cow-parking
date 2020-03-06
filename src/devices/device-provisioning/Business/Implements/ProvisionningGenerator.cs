using System;
using System.Security.Cryptography;
using System.Text;

namespace DeviceProvisioning.Business.Implements
{
    public class ProvisioningGenerator
    {
        // As I am not sure what is doing this scope, I let it as is.
        private static string idScope = Environment.GetEnvironmentVariable("SCOPE_ID"); // "0ne000CEB9C";
        private const string GlobalDeviceEndpoint = "global.azure-devices-provisioning.net";
        private static string EnrollmentGroupPrimaryKey = Environment.GetEnvironmentVariable("PRIMARY_KEY"); // "NrEVzOt8iXlv4LlWkFnQms /PPFreOvaHTv84LtHmTvMoSN/GFKiKu3fV7ZjWpuZpBRgrkWDRvbUKjUiMjUFHAg==";
        private static string EnrollmentGroupSecondaryKey = Environment.GetEnvironmentVariable("SECONDARY_KEY"); //"ozi3vfEQ/mLsPVGBAz7Dz3Utne9sR+sshIMaCI5tlpGyaxuwQ1JpRgxiqm4ZBxwy/uY5oz8exztdQA/piHdtrw==";

        public ProvisioningGeneratorResult Generate(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(idScope))
            {
                Console.WriteLine("ProvisioningDeviceClientSymmetricKey <IDScope>");
                return ProvisioningGeneratorResult.MissingScopeId;
            }

            string primaryKey = "";
            string secondaryKey = "";
            if (CannotGenerateKeys(deviceId))
            {
                primaryKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(EnrollmentGroupPrimaryKey), deviceId);
                secondaryKey = ComputeDerivedSymmetricKey(Convert.FromBase64String(EnrollmentGroupSecondaryKey), deviceId);
            }
            else
            {
                Console.WriteLine("Invalid configuration provided, must provide group enrollment keys or individual enrollment keys");
                return ProvisioningGeneratorResult.CannotGenerateKeys;
            }

            return

            new ProvisioningGeneratorResult(idScope, primaryKey, secondaryKey);
        }

        private bool CannotGenerateKeys(string deviceId)
        {
            return !String.IsNullOrEmpty(deviceId) && !String.IsNullOrEmpty(EnrollmentGroupPrimaryKey) && !String.IsNullOrEmpty(EnrollmentGroupSecondaryKey);
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

    public enum ProvisioningGeneratorResultType
    {
        MissingScopeId,
        CannotGenerateKeys,
        Ok
    }
    public class ProvisioningGeneratorResult
    {
        private ProvisioningGeneratorResult(ProvisioningGeneratorResultType type)
        {
            Type = type;
        }

        internal ProvisioningGeneratorResult(string idScope, string primaryKey, string secondaryKey)
        {
            this.Type = ProvisioningGeneratorResultType.Ok;
            this.GlobalDeviceEndpoint = GlobalDeviceEndpoint;
            this.IdScope = idScope;
            this.PrimaryKey = primaryKey;
            this.SecondaryKey = secondaryKey;
        }

        public ProvisioningGeneratorResultType Type;

        public string GlobalDeviceEndpoint { get; }
        public string IdScope { get; }
        public string PrimaryKey { get; }
        public string SecondaryKey { get; }

        internal static ProvisioningGeneratorResult MissingScopeId => new ProvisioningGeneratorResult(ProvisioningGeneratorResultType.MissingScopeId);
        internal static ProvisioningGeneratorResult CannotGenerateKeys => new ProvisioningGeneratorResult(ProvisioningGeneratorResultType.CannotGenerateKeys);
    }
}