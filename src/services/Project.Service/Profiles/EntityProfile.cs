using AutoMapper;
using Entity.Service.Contracts.Request;
using Entity.Service.Contracts.Response;

namespace Entity.Service.Profiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Infrastructure.Data.Tables.Entity, EntityResponse>();
            CreateMap<EntityResponse, Infrastructure.Data.Tables.Entity>();
            CreateMap<Infrastructure.Data.Tables.Entity, EntityCreateResponse>();
            CreateMap<EntityCreateRequest, Infrastructure.Data.Tables.Entity>();
        }
    }
}
