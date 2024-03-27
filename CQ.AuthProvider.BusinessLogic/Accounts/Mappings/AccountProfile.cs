using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts.Mappings
{
    public sealed class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            CreateMap<AccountEfCore, Account>()
                .ForMember(
                destination => destination.Roles,
                options => options.MapFrom(
                    source => source.Roles.Select(r => r.Key)))
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Roles.SelectMany(r => r.Permissions).Select(p => p.Key)));


            CreateMap<AccountMongo, Account>()
                .ForMember(
                destination => destination.Roles,
                options => options.MapFrom(
                    source => source.Roles.Select(r => r.Key)))
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Roles.SelectMany(r => r.Permissions)));
        }
    }
}
