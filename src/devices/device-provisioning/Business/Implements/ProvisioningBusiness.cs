using DeviceProvisioning.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeviceProvisioning.Business.Implements
{
    public class ProvisioningBusiness
    {
        private readonly ConfigurationFileAccess fileAccessor = new ConfigurationFileAccess();
        private readonly ProvisioningGenerator provisioningGenerator = new ProvisioningGenerator();
        private readonly string generatedDeviceId = ReadDeviceId();

        public void SetupDevice()
        {
            var result = provisioningGenerator.Generate(generatedDeviceId);
            if (result.Type != ProvisioningGeneratorResultType.Ok)
            {
                throw new InvalidOperationException($"Cannot generate info for configuration. type : {result.Type}");
            }
            var configuration = new ConfigurationDevice
            {
                DeviceId = ReadDeviceId(),
                GlobalDeviceEndpoint = result.GlobalDeviceEndpoint,
                IdScope = result.IdScope,
                PrimaryKey = result.PrimaryKey,
                SecondaryKey = result.SecondaryKey,
            };
            fileAccessor.StoreConfiguration(configuration);

            // TODO: Use ProvisioningDeviceClient to register in azure
        }

        // If file exists, it means that device is already registered and nothing is needed
        public bool IsAlreadyRegistered
        {
            get
            {
                if (!fileAccessor.IsAlreadyRegistered)
                {
                    return false;
                }
                else
                {
                    return IsDeviceIdFromConfigIsMatching();
                };
            }
        }

        private bool IsDeviceIdFromConfigIsMatching()
        {
            var configuration = fileAccessor.GetConfiguration();
            return generatedDeviceId != configuration?.DeviceId;
        }

        private static string ReadDeviceId()
        {
            var deviceInfos = File.ReadAllText("/proc/cpuinfo");
            var match = Regex.Match(deviceInfos, @"Serial\s*:\s([0-9a-f]{16})");

            if (match.Success)
            {
                return match.Value.Replace(" ", "")
                                             .Split(":").LastOrDefault();
            }

            throw new InvalidOperationException("Impossible to extract serial number from /proc/cpuinfo file");
        }
    }
}
