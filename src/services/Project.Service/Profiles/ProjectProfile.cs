using AutoMapper;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;

namespace Entity.Service.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Infrastructure.Data.Tables.Entity, ProjectResponse>();
            CreateMap<ProjectResponse, Infrastructure.Data.Tables.Entity>();
            CreateMap<Infrastructure.Data.Tables.Entity, ProjectCreateResponse>();
            CreateMap<ProjectCreateRequest, Infrastructure.Data.Tables.Entity>();
        }
    }
}
