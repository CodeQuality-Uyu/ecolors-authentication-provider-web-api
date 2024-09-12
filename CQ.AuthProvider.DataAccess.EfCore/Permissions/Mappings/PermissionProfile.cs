using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions.Mappings;

internal sealed class PermissionProfile
    : Profile
{
    public PermissionProfile()
    {
        #region Get all
        this.CreatePaginationMap<PermissionEfCore, Permission>();
        #endregion
    }
}
