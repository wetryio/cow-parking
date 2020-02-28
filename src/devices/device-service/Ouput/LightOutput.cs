using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;

namespace device_service.Ouput
{
    public interface ILightOutput
    {
        Task HandleColor(int r, int g, int b); 
    }

    public class LightOutput : ILightOutput
    {
        private DeviceClient deviceClient;

        public LightOutput(DeviceClient deviceClient)
        {
            this.deviceClient = deviceClient;
        }

        public Task HandleColor(int r, int g, int b)
        {
            throw new System.NotImplementedException();
        }
    }
}
