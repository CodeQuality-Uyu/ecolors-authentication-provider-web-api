using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Roles.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Roles.Mappings;

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
