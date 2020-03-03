using AutoMapper;
using Project.Service.Contracts.Request;
using Project.Service.Contracts.Response;

namespace Project.Service.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Infrastructure.Data.Tables.Project, ProjectResponse>();
            CreateMap<ProjectResponse, Infrastructure.Data.Tables.Project>();
            CreateMap<Infrastructure.Data.Tables.Project, ProjectCreateResponse>();
            CreateMap<ProjectCreateRequest, Infrastructure.Data.Tables.Project>();
        }
    }
}
