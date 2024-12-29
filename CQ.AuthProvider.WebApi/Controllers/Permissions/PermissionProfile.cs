using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Permissions;

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
