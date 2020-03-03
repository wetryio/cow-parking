using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProvisioningService.Management.Client.Business;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> logger;
        private readonly IIotEdgeProvisioningBusiness iotEdgeBusiness;

        public TestController(ILogger<TestController> logger, IIotEdgeProvisioningBusiness iotEdgeBusiness)
        {
            this.logger = logger;
            this.iotEdgeBusiness = iotEdgeBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await iotEdgeBusiness.CreateEnrollementGroup("newEnrollement");
            return Ok();
        }
    }
}
