using Microsoft.Extensions.Logging;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;
using Entity.Service.Repositories;
using System.Threading.Tasks;

namespace Entity.Service.Business.Implements
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
