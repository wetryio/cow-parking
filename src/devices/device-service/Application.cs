using device_service.Ouput;
using device_service.Sensors;
using Microsoft.Azure.Devices.Client;
using System;
using System.Threading.Tasks;

namespace device_service
{
    public class Application
    {
        private readonly IHcSrHandler hcSrHandler;
        private readonly ILightOutput lightOutput;

        public Application(IHcSrHandler hcSrHandler, ILightOutput lightOutput)
        {
            this.hcSrHandler = hcSrHandler;
            this.lightOutput = lightOutput;
        }

        public async Task Run()
        {
            await hcSrHandler.StartHandle();
        }
    }
}
