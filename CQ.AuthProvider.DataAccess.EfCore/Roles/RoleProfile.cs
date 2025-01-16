using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        CreateMap<RoleEfCore, Role>()
            .ForMember(
            destination => destination.App,
            options => options.MapFrom<RoleAppResolver>());

        this.CreateOnlyPaginationMap<RoleEfCore, Role>();
        #endregion
    }
}

internal sealed class RoleAppResolver
    : IValueResolver<RoleEfCore, Role, App>
{
    public App Resolve(
        RoleEfCore source,
        Role destination,
        App destMember,
        ResolutionContext context)
    {
        var app = context.Mapper.Map<App>(source.App);

        return app ?? new App { Id = source.AppId };
    }
}