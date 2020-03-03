using Project.Service.Contracts.Request;
using Project.Service.Contracts.Response;
using System.Threading.Tasks;

namespace Project.Service.Business
{
    public interface IProjectBusiness
    {
        Task<ProjectResponse[]> GetProjects(string userId);
        Task<ProjectResponse> CreateProject(ProjectCreateRequest request, string userId);
    }
}
