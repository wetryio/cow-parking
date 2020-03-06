using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeviceProvisioning.Business.Implements
{
    public class ProvisioningBusiness
    {
        private readonly ConfigurationFileAccess fileAccessor = new ConfigurationFileAccess();

        private bool IsDeviceIdFromConfigIsMatching()
        {
            var ownDeviceId = ReadDeviceId();
            var configuration = fileAccessor.GetConfiguration();
            return ownDeviceId != configuration.DeviceId;
        }

        public void SetupDevice()
        {

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
