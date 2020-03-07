using ProvisioningService.Management.Client.Contracts.Request;
using ProvisioningService.Management.Client.Repositories;
using System;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Business.Implements
{
    public class DeviceBusiness : IDeviceBusiness
    {
        private readonly IDeviceRepository deviceRepository;
        private readonly IIotEdgeProvisioningBusiness iotEdgeProvisioningBusiness;

        public DeviceBusiness(IDeviceRepository deviceRepository, IIotEdgeProvisioningBusiness iotEdgeProvisioningBusiness)
        {
            this.deviceRepository = deviceRepository;
            this.iotEdgeProvisioningBusiness = iotEdgeProvisioningBusiness;
        }

        public async Task<int> ProvisionDevices(DeviceProvisioningRequest request)
        {
            int deviceInitilise = 0;
            var escapeName = request.EntityName
                                .Replace(" ", "")
                                .Replace("-", "");

            (string pk, string sk) = await iotEdgeProvisioningBusiness.CreateEnrollementGroup(escapeName);

            var devices = await deviceRepository.GetAvailableDevices(request.DeviceCount);

            foreach(var device in devices)
            {
                if(await iotEdgeProvisioningBusiness.SetupDevice(device.DeviceId, pk, sk, escapeName))
                {
                    deviceInitilise += 1;
                    device.EntityId = request.EntityId;
                    await deviceRepository.UpdateDevice(device);
                }
            }

            return deviceInitilise;
        }
    }
}
