namespace Message.Worker.Infrastructure.Options
{
    public class ServiceSettings
    {
        public IotHubEvent IotHubEvent { get; set; }
    }

    public class IotHubEvent
    {
        public string EventHubsCompatibleEndpoint { get; set; }
        public string EventHubsCompatiblePath { get; set; }
        public string IotHubSasKey { get; set; }
        public string IotHubSasKeyName { get; set; }
    }
}
