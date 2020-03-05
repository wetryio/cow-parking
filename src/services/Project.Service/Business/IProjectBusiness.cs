using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;
using System.Threading.Tasks;

namespace Entity.Service.Business
{
    public interface IProjectBusiness
    {
        Task<ProjectResponse[]> GetProjects(string userId);
        Task<ProjectResponse> CreateProject(ProjectCreateRequest request, string userId);
    }
}
