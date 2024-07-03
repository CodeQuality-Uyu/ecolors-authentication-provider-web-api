using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.DataAccess.Mongo.Permissions.Mappings;

internal sealed class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<PermissionMongo, Permission>()
            .ForMember(destination => destination.Key,
            options => options.MapFrom(
                source => new PermissionKey(source.Key)));

        CreateMap<List<PermissionMongo>, List<Permission>>()
            .ConvertUsing((source, destination, context) =>
            source.Select(p => context.Mapper.Map<Permission>(p)).ToList());
    }
}
