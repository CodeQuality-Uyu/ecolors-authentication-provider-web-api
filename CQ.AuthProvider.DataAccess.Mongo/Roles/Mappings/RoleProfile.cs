
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

namespace CQ.AuthProvider.DataAccess.Mongo.Roles.Mappings;

internal sealed class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleMongo, Role>()
            .ForMember(
            destination => destination.Permissions,
            options => options.MapFrom(
                source => source.Permissions.Select(p => new PermissionKey(p)).ToList()))

            .ForMember(
            destination => destination.Key,
            options => options.MapFrom(
                source => new RoleKey(source.Key)));

        CreateMap<List<RoleMongo>, List<Role>>()
            .ConvertUsing((source, destination, context) =>
            source.Select(r => context.Mapper.Map<Role>(r)).ToList());
    }
}
