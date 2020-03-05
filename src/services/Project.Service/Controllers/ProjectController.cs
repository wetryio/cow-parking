using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entity.Service.Business;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Entity.Service.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> logger;
        private readonly IProjectBusiness projectBusiness;

        public ProjectController(ILogger<ProjectController> logger, IProjectBusiness projectBusiness)
        {
            this.logger = logger;
            this.projectBusiness = projectBusiness;
        }


        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/applepay/validate", Name = "MerchantValidation")]
        public async Task<IActionResult> Get()
        {
            return Ok(await projectBusiness.GetProjects(""));
        }
    }
}
