namespace Message.Worker.Infrastructure.Options
{
    public class ServiceSettings
    {
        public IotHubEvent IotHubEvent { get; set; }
    }

    public class IotHubEvent
    {
        // sb://iothub-ns-cowhub-3014708-5e2087e6e4.servicebus.windows.net/
        public string EventHubsCompatibleEndpoint { get; set; }
        // cowhub
        public string EventHubsCompatiblePath { get; set; }
        // 9goOKF7Huz0mX148J/wwRP3fM3Kym8h3DJyBG+xUWj0=
        public string IotHubSasKey { get; set; }
        // service
        public string IotHubSasKeyName { get; set; }
    }
}
