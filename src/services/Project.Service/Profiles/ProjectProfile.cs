using AutoMapper;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;

namespace Entity.Service.Profiles
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
