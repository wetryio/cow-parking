using Microsoft.EntityFrameworkCore;

namespace Entity.Service.Infrastructure.Data
{
    public class EntityDbContext : DbContext
    {
        public DbSet<Tables.Entity> Entity { get; set; }

        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options)
        {

        }

    }
}
