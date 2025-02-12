using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Blobs;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

internal sealed class TenantProfile
    : Profile
{
    public TenantProfile()
    {
        #region Create session
        CreateMap<Tenant, TenantOfAccountBasicInfoResponse>()
            .ForMember(destination => destination.MiniLogo,
            options => options.MapFrom<MiniLogoMultimediaResolver>())

            .ForMember(destination => destination.CoverLogo,
            options => options.MapFrom<CoverLogoMultimediaResolver>());
        #endregion

        #region Get paginated
        this.CreatePaginationMap<Tenant, TenantBasicInfoResponse>();
        CreateMap<Account, OwnerTenantBasicInfoResponse>();
        #endregion
    }
}

internal sealed class MiniLogoMultimediaResolver(IBlobService _blobService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, BlobReadResponse>
{
    public BlobReadResponse Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        BlobReadResponse destMember,
        ResolutionContext context)
    {
        var (Id, Key, ReadUrl) = _blobService.GetReadElementInTenant(source, source.MiniLogoId);

        return new BlobReadResponse
        {
            Id = Id,
            Key = Key,
            ReadUrl = ReadUrl,
        };
    }
}

internal sealed class CoverLogoMultimediaResolver(IBlobService _blobService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, BlobReadResponse>
{
    public BlobReadResponse Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        BlobReadResponse destMember,
        ResolutionContext context)
    {
        var (Id, Key, ReadUrl) = _blobService.GetReadElementInTenant(source, source.CoverLogoId);

        return new BlobReadResponse
        {
            Id = Id,
            Key = Key,
            ReadUrl = ReadUrl,
        };
    }
}