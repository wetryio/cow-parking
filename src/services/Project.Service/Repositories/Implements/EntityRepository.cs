using Entity.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Service.Repositories.Implements
{
    public class EntityRepository : IEntityRepository
    {
        private readonly EntityDbContext entityDbContext;

        public EntityRepository(EntityDbContext entityDbContext)
        {
            this.entityDbContext = entityDbContext;
        }

        public Task<int> Create(Infrastructure.Data.Tables.Entity entity)
        {
            entityDbContext.Entity.Add(entity);
            return entityDbContext.SaveChangesAsync();
        }

        public Task<Infrastructure.Data.Tables.Entity[]> GetEntities(string userId)
        {
            return entityDbContext.Entity.Where(e => e.CreateBy == userId).ToArrayAsync();
        }

        public Task<Infrastructure.Data.Tables.Entity> GetEntity(Guid entityId)
        {
            return entityDbContext.Entity.FirstOrDefaultAsync(e => e.Id == entityId);
        }
    }
}
