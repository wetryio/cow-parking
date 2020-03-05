using Message.Worker.Infrastructure.Options;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Message.Worker.Tasks
{
    public class MessageTask : BackgroundService
    {
        private readonly ILogger<MessageTask> logger;

        private readonly string eventHubsCompatibleEndpoint;
        private readonly string eventHubsCompatiblePath;
        private readonly string iotHubSasKey;
        private readonly string iotHubSasKeyName;
        private EventHubClient eventHubClient;

        public MessageTask(ILogger<MessageTask> logger, IOptions<ServiceSettings> options)
        {
            this.logger = logger;
            var iotHubEvent = options?.Value?.IotHubEvent;

            if (iotHubEvent == null)
                throw new ArgumentNullException();

            this.eventHubsCompatibleEndpoint = iotHubEvent.EventHubsCompatibleEndpoint;
            this.eventHubsCompatiblePath = iotHubEvent.EventHubsCompatiblePath;
            this.iotHubSasKey = iotHubEvent.IotHubSasKey;
            this.iotHubSasKeyName = iotHubEvent.IotHubSasKeyName;

            InitClient();
        }

        private void InitClient()
        {
            var connectionString = new EventHubsConnectionStringBuilder(new Uri(eventHubsCompatibleEndpoint), eventHubsCompatiblePath, iotHubSasKeyName, iotHubSasKey);
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString.ToString());
        }

        private async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken stoppingToken)
        {
            var eventHubReceiver = eventHubClient.CreateReceiver("$Default", partition, EventPosition.FromEnqueuedTime(DateTime.Now));

            while (true)
            {
                if (stoppingToken.IsCancellationRequested) break;

                var events = await eventHubReceiver.ReceiveAsync(100);

                if (events == null) continue;

                await ProcessEventsAsync(events, partition);
            }
        }

        private async Task ProcessEventsAsync(IEnumerable<EventData> events, string partition)
        {
            foreach (EventData eventData in events)
            {
                string data = Encoding.UTF8.GetString(eventData.Body.Array);
                Console.WriteLine("Message received on partition {0}:", partition);
                Console.WriteLine("  {0}:", data);
                Console.WriteLine("Application properties (set by device):");
                foreach (var prop in eventData.Properties)
                {
                    Console.WriteLine("  {0}: {1}", prop.Key, prop.Value);
                }
                Console.WriteLine("System properties (set by IoT Hub):");
                foreach (var prop in eventData.SystemProperties)
                {
                    Console.WriteLine("  {0}: {1}", prop.Key, prop.Value);
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var runtimeInfo = await eventHubClient.GetRuntimeInformationAsync();
            var d2cPartitions = runtimeInfo.PartitionIds;

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, stoppingToken));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
