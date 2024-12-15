using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants.Mappings;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        CreateMap<TenantEfCore, Tenant>()
            .ForMember(
            destination => destination.Owner,
            options => options.MapFrom(
                source => new Account
                {
                    Id = source.OwnerId,
                }));
    }
}