using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions.Mappings;

internal sealed class PermissionProfile
    : Profile
{
    public PermissionProfile()
    {
        #region Get all
        CreateMap<PermissionEfCore, Permission>();
        //CreateMap<string, PermissionKey>()
        //    .ConvertUsing((source, destination, context) => new PermissionKey(source));
        #endregion
    }
}
