using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions.Mappings;

internal sealed class PermissionProfile
    : Profile
{
    public PermissionProfile()
    {
        #region Get all
        CreateMap<PermissionEfCore, Permission>()
            .ConvertUsing((source, destination, options) => new Permission
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Key = source.Key,
                IsPublic = source.IsPublic
            });
        #endregion

        CreateMap<RolePermission, Permission>()
            .ConvertUsing((source, destination, options) => new Permission
            {
                Id = source.PermissionId,
                Name = source.Permission.Name,
                Description = source.Permission.Description,
                IsPublic = source.Permission.IsPublic,
                Key = source.Permission.Key
            });
    }
}
