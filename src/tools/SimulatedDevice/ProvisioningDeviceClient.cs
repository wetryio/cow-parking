using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimulatedDevice
{
    public class ProvisioningDevice
    {
        ProvisioningDeviceClient _provClient;
        SecurityProvider _security;

        private static DeviceClient deviceClient;


        public ProvisioningDevice(ProvisioningDeviceClient provisioningDeviceClient, SecurityProvider security)
        {
            _provClient = provisioningDeviceClient;
            _security = security;
        }

        public async Task RunSampleAsync()
        {
            Console.WriteLine($"RegistrationID = {_security.GetRegistrationID()}");
            VerifyRegistrationIdFormat(_security.GetRegistrationID());

            Console.Write("ProvisioningClient RegisterAsync . . . ");
            DeviceRegistrationResult result = await _provClient.RegisterAsync().ConfigureAwait(false);

            Console.WriteLine($"{result.Status}");
            Console.WriteLine($"ProvisioningClient AssignedHub: {result.AssignedHub}; DeviceID: {result.DeviceId}");

            if (result.Status != ProvisioningRegistrationStatusType.Assigned) return;

            IAuthenticationMethod auth;
            if (_security is SecurityProviderSymmetricKey)
            {
                Console.WriteLine("Creating Symmetric Key DeviceClient authenication");
                auth = new DeviceAuthenticationWithRegistrySymmetricKey(result.DeviceId, (_security as SecurityProviderSymmetricKey).GetPrimaryKey());
            }
            else
            {
                throw new NotSupportedException("Unknown authentication type.");
            }

            deviceClient = DeviceClient.Create(result.AssignedHub, auth, TransportType.Amqp);
            await deviceClient.OpenAsync().ConfigureAwait(false);
            SendDeviceToCloudMessagesAsync();
        }

        private void VerifyRegistrationIdFormat(string v)
        {
            var r = new Regex("^[a-z0-9-]*$");
            if (!r.IsMatch(v))
            {
                throw new FormatException("Invalid registrationId: The registration ID is alphanumeric, lowercase, and may contain hyphens");
            }
        }

        private async void SendDeviceToCloudMessagesAsync()
        {
            while (true)
            {
                bool hasObstacle = GetRandomObstacle();
                double batteryLevel = GetRandomBatteryLevel();

                var telemetryDataPoint = new
                {
                    hasObstacle = hasObstacle,
                    batteryLevel = batteryLevel
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(NextTelemetry() * 100);
            }
        }

        private double GetRandomBatteryLevel()
        {
            Random gen = new Random();
            int prob = gen.Next(100);
            return prob;
        }

        private int NextTelemetry()
        {
            Random gen = new Random();
            int prob = gen.Next(100, 600);
            return prob;
        }

        private bool GetRandomObstacle()
        {
            Random gen = new Random();
            int prob = gen.Next(100);
            return prob <= 40;
        }
    }
}
