namespace DeviceProvisioning.Models
{
    public class ConfigurationDevice
    {
        public string DeviceId { get; set; }
        public string GlobalDeviceEndpoint { get; set; }
        public string IdScope { get; set; }
        public string PrimaryKey { get; set; }
        public string SecondaryKey { get; set; }
    }
}