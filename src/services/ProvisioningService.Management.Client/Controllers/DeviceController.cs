using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProvisioningService.Management.Client.Business;
using ProvisioningService.Management.Client.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> logger;
        private readonly IDeviceBusiness deviceBusiness;

        public DeviceController(ILogger<DeviceController> logger, IDeviceBusiness deviceBusiness)
        {
            this.logger = logger;
            this.deviceBusiness = deviceBusiness;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> ProvisionDevices([FromBody]DeviceProvisioningRequest request)
        {
            if (await deviceBusiness.ProvisionDevices(request) == request.DeviceCount)
                return Ok();

            return Accepted();
        }
    }
}
