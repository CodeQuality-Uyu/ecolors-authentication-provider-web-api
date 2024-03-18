using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ClientSystems.Mappings
{
    internal sealed class ClientSystemProfile : Profile
    {
        public ClientSystemProfile() 
        {
            CreateMap<ClientSystemEfCore, ClientSystem>()
                .ForMember(
                destination => destination.Role,
                options => options.MapFrom(
                    source => new RoleKey(source.Role.Key)))
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Role.Permissions.Select(p => new PermissionKey(p.Key))));

            CreateMap<ClientSystemMongo, ClientSystem>()
                .ForMember(
                destination => destination.Role,
                options => options.MapFrom(
                    source => new RoleKey(source.Role.Key)))
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Role.Permissions.Select(p => new PermissionKey(p))));
        }
    }
}
