using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;
using System;
using System.Threading.Tasks;

namespace Entity.Service.Business
{
    public interface IEntityBusiness
    {
        Task<EntityResponse[]> GetEntities(string userId);
        Task<EntityResponse> CreateEntity(EntityCreateRequest request, string userId);
        Task<EntityResponse> GetEntity(Guid entityId);
    }
}
