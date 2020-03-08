using System;
using System.ComponentModel.DataAnnotations;

namespace ProvisioningService.Management.Client.Infrastructure.Data.Tables
{
    public class DeviceRegistration
    {
        [Key]
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public Guid? EntityId { get; set; }
    }
}
