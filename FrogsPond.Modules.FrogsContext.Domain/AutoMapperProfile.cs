using AutoMapper;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;

namespace FrogsPond.Modules.FrogsContext.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Frog, FrogResponse>();

            CreateMap<FrogCreateRequest, Frog>();

            CreateMap<FrogUpdateRequest, Frog>();
        }
    }
}
