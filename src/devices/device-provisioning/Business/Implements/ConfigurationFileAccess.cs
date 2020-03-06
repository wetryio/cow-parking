using System.IO;
using DeviceProvisioning.Models;
using System.Text.Json;

namespace DeviceProvisioning.Business.Implements
{
    public class ConfigurationFileAccess
    {
        private const string ConfigurationFile = "/etc/cow-device.conf";

        // If file exists, it means that device is already registered and nothing is needed
        public bool IsAlreadyRegistered => File.Exists(ConfigurationFile);

        public ConfigurationDevice GetConfiguration()
        {
            return JsonSerializer.Deserialize<ConfigurationDevice>(File.ReadAllText(ConfigurationFile));
        }

        private static void StoreConfiguration(ConfigurationDevice configuration)
        {
            File.WriteAllText(ConfigurationFile, JsonSerializer.Serialize(configuration));
        }
    }
}
