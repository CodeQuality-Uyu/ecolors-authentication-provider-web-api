using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles.Mappings;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        CreateMap<RoleEfCore, Role>();
        #endregion
    }
}