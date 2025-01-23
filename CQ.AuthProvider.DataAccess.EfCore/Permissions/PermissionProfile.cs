using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

internal sealed class PermissionProfile
    : Profile
{
    public PermissionProfile()
    {
        #region Get all
        CreateMap<PermissionEfCore, Permission>()
            .ForMember(destination => destination.App,
            options => options.MapFrom<AppResolver>());
        this.CreateOnlyPaginationMap<PermissionEfCore, Permission>();
        #endregion
    }
}

internal sealed class AppResolver
    : IValueResolver<PermissionEfCore, Permission, App>
{
    public App Resolve(
        PermissionEfCore source,
        Permission destination,
        App destMember,
        ResolutionContext context)
    {
        return new App
        {
            Id = source.AppId
        };
    }
}
