using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        #region Create
        CreateMap<App, AppEfCore>()
            .ForMember(destination => destination.Tenant,
            options => options.Ignore())
            .ForMember(destination => destination.TenantId,
            options => options.MapFrom(
                source => source.Tenant.Id));
        #endregion

        #region GetById
        CreateMap<AppEfCore, App>()
            .ForMember(
            destination => destination.Tenant,
            options => options.MapFrom<TenantResolver>());

        CreateMap<CoverBackgroundColorEfCore, CoverBackgroundColor>();
        #endregion

        #region GetAll
        this.CreateOnlyPaginationMap<AppEfCore, App>();
        #endregion
    }
}

internal sealed class TenantResolver
    : IValueResolver<AppEfCore, App, Tenant>
{
    public Tenant Resolve(
        AppEfCore source,
        App destination,
        Tenant destMember,
        ResolutionContext context) => new ()
        {
            Id = source.TenantId,
            Name = source.Tenant?.Name
        };
}
