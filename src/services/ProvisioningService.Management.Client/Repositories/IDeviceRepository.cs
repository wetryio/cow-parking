using ProvisioningService.Management.Client.Infrastructure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Repositories
{
    public interface IDeviceRepository
    {
        Task<DeviceRegistration[]> GetAvailableDevices(int count);
        Task<int> UpdateDevice(DeviceRegistration deviceRegistration);
    }
}
