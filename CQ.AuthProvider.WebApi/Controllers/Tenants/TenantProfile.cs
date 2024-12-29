using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        #region Create session
        CreateMap<Tenant, TenantOfAccountBasicInfoResponse>();
        #endregion
    }
}
