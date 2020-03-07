using Entity.Service.Business;
using Entity.Service.Contracts.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Core;
using System;
using System.Threading.Tasks;

namespace Entity.Service.Controllers
{
    [Route("api/[controller]")]
    public class EntityController : CoreController
    {
        private readonly ILogger<EntityController> logger;
        private readonly IEntityBusiness entityBusiness;

        public EntityController(ILogger<EntityController> logger, IEntityBusiness entityBusiness)
        {
            this.logger = logger;
            this.entityBusiness = entityBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation($"Retrive entities for user {GetObjectId}");
            return Ok(await entityBusiness.GetEntities(GetObjectId));
        }

        [HttpGet("{entityId}")]
        public async Task<IActionResult> Get([FromRoute]Guid entityId)
        {
            logger.LogInformation($"Retrive entities for entityid {entityId}");
            return Ok(await entityBusiness.GetEntity(entityId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EntityCreateRequest request)
        {
            logger.LogInformation($"Create entity for user {GetObjectId}");
            return Ok(await entityBusiness.CreateEntity(request, GetObjectId));
        }
    }
}
