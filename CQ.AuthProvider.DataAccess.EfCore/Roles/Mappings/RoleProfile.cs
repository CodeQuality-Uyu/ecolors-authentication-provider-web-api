using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles.Mappings;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        this.CreatePaginationMap<RoleEfCore, Role>();
        #endregion
    }
}