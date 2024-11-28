using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants.Mappings;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        CreateMap<TenantEfCore, Tenant>()
            .ForMember(
            destination => destination.Owner,
            options => options.ConvertUsing<TenantOwnerResolver, AccountEfCore>(tenant => tenant.Owner));
    }
}

internal sealed class TenantOwnerResolver()
    : IValueConverter<AccountEfCore, Account>
{
    public Account Convert(AccountEfCore sourceMember, ResolutionContext context)
    {
        if(Guard.IsNull(sourceMember))
        {
            var ownerId = (Guid)context.Items[nameof(TenantEfCore.OwnerId)];

            return new Account
            {
                Id = ownerId
            };
        }

        return context.Mapper.Map<Account>(sourceMember);
    }
}
