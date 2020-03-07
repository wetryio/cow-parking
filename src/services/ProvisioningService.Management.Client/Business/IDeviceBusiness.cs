using ProvisioningService.Management.Client.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Business
{
    public interface IDeviceBusiness
    {
        Task<int> ProvisionDevices(DeviceProvisioningRequest request);
    }
}
