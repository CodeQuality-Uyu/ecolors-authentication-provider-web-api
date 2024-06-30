using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.DataAccess.Mongo.Permissions.Mappings;

internal sealed class PermissionKeyProfile : Profile
{
    public PermissionKeyProfile()
    {
        CreateMap<List<PermissionKey>, List<string>>()
            .ConvertUsing(source => source.Select(p => p.ToString()).ToList());
    }
}
