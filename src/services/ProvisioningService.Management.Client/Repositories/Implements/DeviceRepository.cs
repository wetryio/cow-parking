using Microsoft.EntityFrameworkCore;
using ProvisioningService.Management.Client.Infrastructure.Data;
using ProvisioningService.Management.Client.Infrastructure.Data.Tables;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Repositories.Implements
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext deviceDbContext;

        public DeviceRepository(DeviceDbContext deviceDbContext)
        {
            this.deviceDbContext = deviceDbContext;
        }

        public Task<DeviceRegistration[]> GetAvailableDevices(int count)
        {
            return deviceDbContext.DeviceRegistration.Where(d => d.EntityId == null)
                    .Take(count)
                    .ToArrayAsync();
        }

        public Task<int> UpdateDevice(DeviceRegistration deviceRegistration)
        {
            deviceDbContext.Entry(deviceRegistration).State = EntityState.Modified;
            return deviceDbContext.SaveChangesAsync();
        }
    }
}
