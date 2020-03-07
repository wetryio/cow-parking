using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Infrastructure.Data.Tables
{
    public class DeviceRegistration
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public Guid EntityId { get; set; }
    }
}
