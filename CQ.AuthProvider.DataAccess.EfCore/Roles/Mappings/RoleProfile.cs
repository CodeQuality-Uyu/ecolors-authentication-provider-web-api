using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles.Mappings;

internal sealed class RoleProfile
    : Profile
{
    public RoleProfile()
    {
        #region Get all
        CreateMap<RoleEfCore, Role>();
        #endregion

        CreateMap<AccountRole, Role>()
           .ConvertUsing((source, destination, options) => options.Mapper.Map<Role>(source.Role));
    }
}