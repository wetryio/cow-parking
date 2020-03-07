using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Contracts.Request
{
    public class DeviceProvisioningRequest
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public int DeviceCount { get; set; }
    }
}
