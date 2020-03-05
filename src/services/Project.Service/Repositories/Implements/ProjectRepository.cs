using Entity.Service.Infrastructure.Data;

namespace Entity.Service.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly EntityDbContext projectDbContext;

        public ProjectRepository(EntityDbContext projectDbContext)
        {
            this.projectDbContext = projectDbContext;
        }
    }
}
