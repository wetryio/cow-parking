using AutoMapper;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;
using Entity.Service.Models;
using Entity.Service.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
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

            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("http://provisionning-service.services.185.136.234.221.xip.io");

                var bodyRequest = JsonConvert.SerializeObject(new DeviceProvisioningRequest()
                {
                    DeviceCount = entityDb.DeviceCount,
                    EntityId = entityDb.Id,
                    EntityName = entityDb.Name
                });


                var response = await hc.PostAsync("/api/device/enroll", new StringContent(bodyRequest, System.Text.Encoding.UTF8, "application/json"));
            }

            return mapper.Map<EntityResponse>(entityDb);
        }

        public async Task<EntityResponse> GetEntity(Guid entityId)
        {
            var entityDb = await entityRepository.GetEntity(entityId);
            var response = mapper.Map<EntityResponse>(entityDb);
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new Uri("https://atlas.microsoft.com/search/address/json");
                var responseGeoService = await hc.GetAsync($"?api-version=1.0&subscription-key=uQoYKL8oYqJSFLOciOLJ1cD3PTti8wlw4wo87oG9Xbs&query={entityDb.PostCode}");
                var geoResponse = JsonConvert.DeserializeObject<GeoServiceResponse>(await responseGeoService.Content.ReadAsStringAsync());

                response.Lat = geoResponse.Results[0]?.Position?.Lat;
                response.Lng = geoResponse.Results[0]?.Position?.Lon;
            }

            return response;
        }

        public async Task<EntityResponse[]> GetEntities(string userId)
        {
            var entitiesDb = await entityRepository.GetEntities(userId);
            return mapper.Map<EntityResponse[]>(entitiesDb);
        }
    }
}
