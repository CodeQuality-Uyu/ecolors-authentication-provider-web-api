using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>();
        CreateMap<AccountRole, Role>()
            .ConvertUsing((source, destination, options) => options.Mapper.Map<Role>(source.Role));

        CreateMap<RolePermission, Permission>()
            .ConvertUsing((source, destination, options) => new Permission
            {
                Id = source.PermissionId,
                Name = source.Permission.Name,
                Description = source.Permission.Description,
                IsPublic = source.Permission.IsPublic,
                Key = options.Mapper.Map<PermissionKey>(source.Permission.Key)
            });

        CreateMap<AccountApp, App>()
            .ConvertUsing((source, destination, options) => options.Mapper.Map<App>(source.App));
        CreateMap<AppEfCore, App>();

        CreateMap<TenantEfCore, Tenant>();
    }
}
