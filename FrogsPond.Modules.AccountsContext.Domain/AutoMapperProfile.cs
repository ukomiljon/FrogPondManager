using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using FrogsPond.Modules.AccountsContext.Domain.Models;
using FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.AccountsContext.Domain
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();

            CreateMap<AccountCreateRequest, Account>();

            CreateMap<AccountUpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
        }
    }
}
