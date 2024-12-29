using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountEfCore, Account>()
            .ForMember(destination => destination.Tenant,
            options => options.MapFrom(
                source => new Tenant
                {
                    Id = source.TenantId,
                }))

            .ForMember(destination => destination.Apps,
            options => options.MapFrom(
                source => source.Apps.ConvertAll(a => new App
                {
                    Id = a.Id,
                })));
        this.CreateOnlyPaginationMap<AccountEfCore, Account>();
    }
}
