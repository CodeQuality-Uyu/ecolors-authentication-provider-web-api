using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;

internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>();
        CreateMap<TenantEfCore, Tenant>();
    }
}
