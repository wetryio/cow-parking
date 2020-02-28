using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;

namespace device_service.Sensors
{
    public interface IHcSrHandler
    {
        Task StartHandle();
    }

    public class HcSrHandler : IHcSrHandler
    {
        private DeviceClient deviceClient;
        private UltrasonicHcsr04 sensorInstance;

        private DateTime hasObstacle;

        public HcSrHandler(DeviceClient deviceClient)
        {
            this.deviceClient = deviceClient;
            this.sensorInstance = new UltrasonicHcsr04(Pi.Gpio[BcmPin.Gpio11], Pi.Gpio[BcmPin.Gpio12]);
            this.sensorInstance.OnDataAvailable += HandleObstacleAsync;
        }

        public async Task StartHandle()
        {
            sensorInstance.Start();
        }

        // To improve
        // Distance > 60 cm (e.Distance)
        private void HandleObstacleAsync(object sender, UltrasonicReadEventArgs e)
        {
            if (hasObstacle == null)
                hasObstacle = DateTime.UtcNow;

            if (hasObstacle > DateTime.UtcNow.AddSeconds(15) && hasObstacle < DateTime.UtcNow.AddSeconds(30))
            {
                var dataBuffer = $"{{\"deviceId\":device01,\"hasObstacle\":true}}";
                Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
                deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);
            }
            else
            {
                if (hasObstacle > DateTime.UtcNow.AddSeconds(30))
                {
                    var dataBuffer = $"{{\"deviceId\":device01,\"hasObstacle\":false}}";
                    Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
                    deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);
                }
            }
        }
    }
}
