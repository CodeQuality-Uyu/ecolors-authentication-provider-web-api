using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Multimedias;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.WebApi.Controllers.Models;

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
    }
}

internal sealed class MiniLogoMultimediaResolver(IMultimediaService _multimediaService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, MultimediaResponse>
{
    public MultimediaResponse Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        MultimediaResponse destMember,
        ResolutionContext context)
    {
        var (Id, ReadUrl, WriteUrl) = _multimediaService.GetById(source.MiniLogoId);

        return new MultimediaResponse(
            Id,
            ReadUrl,
            WriteUrl);
    }
}

internal sealed class CoverLogoMultimediaResolver(IMultimediaService _multimediaService)
    : IValueResolver<Tenant, TenantOfAccountBasicInfoResponse, MultimediaResponse>
{
    public MultimediaResponse Resolve(
        Tenant source,
        TenantOfAccountBasicInfoResponse destination,
        MultimediaResponse destMember,
        ResolutionContext context)
    {
        var (Id, ReadUrl, WriteUrl) = _multimediaService.GetById(source.CoverLogoId);

        return new MultimediaResponse(
            Id,
            ReadUrl,
            WriteUrl);
    }
}