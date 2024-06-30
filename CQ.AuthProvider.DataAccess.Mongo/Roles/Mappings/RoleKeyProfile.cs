using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

namespace CQ.AuthProvider.DataAccess.Mongo.Roles.Mappings;

internal sealed class RoleKeyProfile : Profile
{
    public RoleKeyProfile()
    {
        CreateMap<List<RoleKey>, List<string>>()
            .ConvertUsing(source => source.ConvertAll(r => r.ToString()));
    }
}
