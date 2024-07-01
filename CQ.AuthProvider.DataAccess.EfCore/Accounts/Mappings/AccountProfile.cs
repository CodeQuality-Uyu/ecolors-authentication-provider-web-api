using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;

internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>();
        CreateMap<RoleEfCore, Role>();
        CreateMap<PermissionEfCore, Permission>();
        CreateMap<TenantEfCore, Tenant>();
    }
}
