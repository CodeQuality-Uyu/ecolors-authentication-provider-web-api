using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        #region GetPaginated
        this.CreateOnlyPaginationMap<TenantEfCore, Tenant>();
        #endregion

        CreateMap<TenantEfCore, Tenant>();

        #region CreateAndSave
        CreateMap<Tenant, TenantEfCore>();
        #endregion
    }
}