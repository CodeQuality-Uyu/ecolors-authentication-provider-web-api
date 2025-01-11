using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        #region GetPaginated
        this.CreateOnlyPaginationMap<TenantEfCore, Tenant>();
        #endregion

        CreateMap<TenantEfCore, Tenant>()
            .ForMember(
            destination => destination.Owner,
            options => options.MapFrom<TenantOwnerResolver>());

        #region CreateAndSave
        CreateMap<Tenant, TenantEfCore>()
            .ForMember(destination => destination.Owner,
            options => options.Ignore())

            .ForMember(destination => destination.OwnerId,
            options => options.MapFrom(
                source => source.Owner.Id));
        #endregion
    }

    internal sealed class TenantOwnerResolver
        : IValueResolver<TenantEfCore, Tenant, Account>
    {
        public Account Resolve(
            TenantEfCore source,
            Tenant destination,
            Account destMember,
            ResolutionContext context)
        {
            var account = context.Mapper.Map<Account>(source.Owner);

            if (Guard.IsNotNull(account))
            {
                return account;
            }

            return new Account
            {
                Id = source.OwnerId,
            };
        }
    }
}