using System;
using System.Threading.Tasks;

namespace Entity.Service.Repositories
{
    public interface IEntityRepository
    {
        Task<Infrastructure.Data.Tables.Entity[]> GetEntities(string userId);
        Task<Infrastructure.Data.Tables.Entity> GetEntity(Guid entityId);
        Task<int> Create(Infrastructure.Data.Tables.Entity entity);
    }
}
