using Microsoft.EntityFrameworkCore;

namespace Entity.Service.Infrastructure.Data
{
    public class EntityDbContext : DbContext
    {
        public DbSet<Tables.Entity> Project { get; set; }

        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options)
        {

        }

    }
}
