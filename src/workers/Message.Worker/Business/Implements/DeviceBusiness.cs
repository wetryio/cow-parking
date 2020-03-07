using Message.Worker.Contracts.Event;
using Message.Worker.Infrastructure.Data.Tables;
using Message.Worker.Repositories;
using Message.Worker.Repositories.Implements;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Message.Worker.Business.Implements
{
    public class DeviceBusiness : IDeviceBusiness
    {
        private IDeviceRepository deviceRepository;

        public DeviceBusiness(IDeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public async Task ProcessDevice(EventData eventData)
        {
            string data = Encoding.UTF8.GetString(eventData.Body.Array);
            var deviceEventData = JsonConvert.DeserializeObject<DeviceEvent>(data);
            var deviceId = (string)eventData.SystemProperties["iothub-connection-device-id"];

            var newDevice = new DeviceState()
            {
                DeviceId = deviceId,
                Obstructed = deviceEventData.HasObstacle,
                BatteryLevel = (int)deviceEventData.BatteryLevel
            };

            await deviceRepository.InsertDevice(newDevice);
        }
    }
}
