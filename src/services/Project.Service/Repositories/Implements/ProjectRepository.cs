using Project.Service.Infrastructure.Data;

namespace Project.Service.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectDbContext projectDbContext;

        public ProjectRepository(ProjectDbContext projectDbContext)
        {
            this.projectDbContext = projectDbContext;
        }
    }
}
