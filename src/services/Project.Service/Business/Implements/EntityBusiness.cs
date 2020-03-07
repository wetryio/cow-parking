using AutoMapper;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;
using Entity.Service.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Entity.Service.Business.Implements
{
    public class EntityBusiness : IEntityBusiness
    {
        private readonly ILogger<EntityBusiness> logger;
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public EntityBusiness(ILogger<EntityBusiness> logger, IEntityRepository entityRepository, IMapper mapper)
        {
            this.logger = logger;
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        public async Task<EntityResponse> CreateEntity(EntityCreateRequest request, string userId)
        {
            var entityDb = mapper.Map<Infrastructure.Data.Tables.Entity>(request);
            entityDb.CreateBy = userId;
            await entityRepository.Create(entityDb);
            return mapper.Map<EntityResponse>(entityDb);
        }

        public async Task<EntityResponse> GetEntity(Guid entityId)
        {
            var entityDb = await entityRepository.GetEntity(entityId);
            return mapper.Map<EntityResponse>(entityDb);
        }

        public async Task<EntityResponse[]> GetEntities(string userId)
        {
            var entitiesDb = await entityRepository.GetEntities(userId);
            return mapper.Map<EntityResponse[]>(entitiesDb);
        }
    }
}
