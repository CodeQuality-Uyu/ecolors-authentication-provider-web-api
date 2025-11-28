using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Blobs;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

internal sealed class TenantMapping
    : Profile
{
    public TenantMapping()
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

internal sealed class MiniLogoMultimediaResolver(IBlobService blobService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        BlobReadResponse? destMember,
        ResolutionContext context)
    {
        if (source.MiniLogoKey == null)
        {
            return null;
        }

        return blobService.GetByKey(source.MiniLogoKey);
    }
}

internal sealed class CoverLogoMultimediaResolver(IBlobService blobService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        BlobReadResponse? destMember,
        ResolutionContext context)
    {
        if (source.CoverLogoKey == null)
        {
            return null;
        }

        return blobService.GetByKey(source.CoverLogoKey);
    }
}