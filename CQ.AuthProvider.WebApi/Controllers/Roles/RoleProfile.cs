using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Roles;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        this.CreatePaginationMap<Role, RoleBasicInfoResponse>();
        #endregion
    }
}
