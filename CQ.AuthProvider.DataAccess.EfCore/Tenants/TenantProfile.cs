using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;

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
            options => options.MapFrom(
                source => new Account
                {
                    Id = source.OwnerId,
                }));

        #region CreateAndSave
        CreateMap<Tenant, TenantEfCore>()
            .ForMember(destination => destination.Owner,
            options => options.Ignore())

            .ForMember(destination => destination.OwnerId,
            options => options.MapFrom(
                source => source.Owner.Id));
        #endregion
    }
}