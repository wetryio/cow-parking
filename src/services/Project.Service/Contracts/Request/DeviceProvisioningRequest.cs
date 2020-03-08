using System;

namespace Entity.Service.Contracts.Request
{
    public class DeviceProvisioningRequest
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public int DeviceCount { get; set; }
    }
}
