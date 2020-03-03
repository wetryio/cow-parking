using Microsoft.Extensions.Logging;
using Project.Service.Contracts.Request;
using Project.Service.Contracts.Response;
using Project.Service.Repositories;
using System.Threading.Tasks;

namespace Project.Service.Business.Implements
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly ILogger<ProjectBusiness> logger;
        private readonly IProjectRepository projectRepository;

        public ProjectBusiness(ILogger<ProjectBusiness> logger, IProjectRepository projectRepository)
        {
            this.logger = logger;
            this.projectRepository = projectRepository;
        }

        public Task<ProjectResponse> CreateProject(ProjectCreateRequest request, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProjectResponse[]> GetProjects(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
