using DeviceProvisioning.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DeviceProvisioning.Business.Implements
{
    public class ProvisioningBusiness
    {
        private const string MasterKey = "Iyppvm9WIbViax1XlyrqW+/5R1ZPf3hyhtXp2ctS+s5AbsvCvnH5ma0V1UxX2GBY+1MnVoQYtsrMppVn4H/I5Q==";
        private const string ScopeId = "0ne000CEB9C";

        public async Task SetupDevice()
        {
            System.Diagnostics.Process.Start("/bin/bash", "-c systemctl stop iotedge");

            var deviceId = ReadDeviceId();
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = $"deviceId-{Guid.NewGuid()}";
                Console.WriteLine($"device id : {deviceId}");
                WriteDeviceId(deviceId);
            }

            var yamlToWrite = GenerateYaml(deviceId, MasterKey, ScopeId);
            await File.WriteAllTextAsync("/etc/iotedge/config.yaml", yamlToWrite);

            System.Diagnostics.Process.Start("/bin/bash", "-c systemctl daemon-reload");
            System.Diagnostics.Process.Start("/bin/bash", "-c reboot");
        }

        private string GenerateYaml(string deviceId, string masterKey, string scopeId)
        {
            String deviceKey =
                    KeyGenerator.ComputeDerivedSymmetricKey(
                               Convert.FromBase64String(masterKey),
                               deviceId);

            var yamlContent = File.ReadAllText("Yaml/iot-edge-config.yaml");

            return yamlContent.Replace("##SCOPEID##", scopeId)
                .Replace("##DEVICEID##", deviceId)
                .Replace("##SYMMETRICKEY##", deviceKey);
        }


        private string ReadDeviceId()
        {
            if (!File.Exists("deviceid.txt"))
                File.Create("deviceid.txt");

            return File.ReadAllText("deviceid.txt");
        }

        private void WriteDeviceId(string deviceId)
        {
            if (!File.Exists("deviceid.txt"))
                File.Create("deviceid.txt");

            File.WriteAllText("deviceid.txt", deviceId);
        }
    }
}
