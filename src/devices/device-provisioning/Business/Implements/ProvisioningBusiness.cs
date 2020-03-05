using DeviceProvisioning.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeviceProvisioning.Business.Implements
{
    public class ProvisioningBusiness
    {
        private const string MasterKey = "Iyppvm9WIbViax1XlyrqW+/5R1ZPf3hyhtXp2ctS+s5AbsvCvnH5ma0V1UxX2GBY+1MnVoQYtsrMppVn4H/I5Q==";
        private const string ScopeId = "0ne000CEB9C";

        public async Task SetupDevice()
        {
            Console.WriteLine(ReadDeviceId());
        }

        private string GenerateYaml(string deviceId, string masterKey, string scopeId)
        {
            String deviceKey =
                    KeyGenerator.ComputeDerivedSymmetricKey(
                               Convert.FromBase64String(masterKey),
                               deviceId);

            return string.Empty;
        }


        private string ReadDeviceId()
        {
            var deviceInfos = File.ReadAllText("/proc/cpuinfo");
            var match = Regex.Match(deviceInfos, @"Serial\s*:\s([0-9a-f]{16})");

            if (match.Success)
            {
                return match.Value.Replace(" ", "")
                                             .Split(":").LastOrDefault();
            }

            return string.Empty;
        }

        private void WriteDeviceId(string deviceId)
        {
            if (!File.Exists("deviceid.txt"))
                File.Create("deviceid.txt");

            File.WriteAllText("deviceid.txt", deviceId);
        }
    }
}
