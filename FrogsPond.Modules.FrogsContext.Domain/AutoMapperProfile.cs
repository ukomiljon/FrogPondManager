using AutoMapper;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
