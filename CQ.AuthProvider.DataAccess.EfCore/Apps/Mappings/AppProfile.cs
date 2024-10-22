using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps.Mappings;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        #region GetById
        CreateMap<AppEfCore, App>()
            .ForMember(
            destination => destination.Tenant,
            options => options.MapFrom(
                source => new Tenant
                {
                    Id = source.TenantId
                }));
        #endregion

        #region GetAll
        this.CreateOnlyPaginationMap<AppEfCore, App>();
        #endregion
    }
}
