using Microsoft.EntityFrameworkCore;

namespace Project.Service.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Tables.Project> Project { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {

        }

    }
}
