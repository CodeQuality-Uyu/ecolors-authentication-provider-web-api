
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations.Mappings
{
    public sealed class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEfCore, RoleInfo>()
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Permissions.Select(p => new PermissionKey(p.Key)).ToList()))

                .ForMember(
                destination => destination.Key,
                options => options.MapFrom(
                    source => new RoleKey(source.Key)));

            CreateMap<RoleMongo, RoleInfo>()
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Permissions.Select(p => new PermissionKey(p)).ToList()))

                .ForMember(
                destination => destination.Key,
                options => options.MapFrom(
                    source => new RoleKey(source.Key)));
        }
    }
}
