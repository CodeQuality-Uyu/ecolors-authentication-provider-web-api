using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Mappings;

internal sealed class PermissionProfile
    : Profile
{
    public PermissionProfile()
    {
        #region Get all

        this.CreatePaginationMap<Permission, PermissionBasicInfoResponse>();
        #endregion
    }
}
