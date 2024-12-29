
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Licenses;

namespace CQ.AuthProvider.DataAccess.EfCore.Licenses;

internal sealed class LicenseProfile
    : Profile
{
    public LicenseProfile()
    {
        CreateMap<License, LicenseEfCore>()
            .ForMember(destination => destination.App,
            options => options.Ignore())
            
            .ForMember(destination => destination.AppId,
            options => options.MapFrom(
                source => source.App.Id))

            .ForMember(destination => destination.Tenant,
            options => options.Ignore())
            
            .ForMember(destination => destination.TenantId,
            options => options.MapFrom(
                source => source.Tenant.Id));

    }
}
