using Microsoft.EntityFrameworkCore;
using ProvisioningService.Management.Client.Infrastructure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Infrastructure.Data
{
    public class DeviceDbContext : DbContext
    {
        public DbSet<DeviceRegistration> DeviceRegistration { get; set; }

        public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
            : base(options)
        {

        }
    }
}
