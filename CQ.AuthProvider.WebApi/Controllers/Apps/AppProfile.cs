using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Multimedias;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        #region GetAll
        this.CreatePaginationMap<App, AppBasicInfoResponse>();
        #endregion

        #region GetById
        CreateMap<App, AppDetailInfoResponse>()
            .ForMember(destination => destination.CoverMultimedia,
            options => options.MapFrom<CoverMultimediaResolver>());

        #endregion
    }
}

internal sealed class CoverMultimediaResolver(IMultimediaService _multimediaService)
    : IValueResolver<App, AppDetailInfoResponse, MultimediaResponse>
{
    public MultimediaResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        MultimediaResponse destMember,
        ResolutionContext context)
    {
        var (Id, ReadUrl, WriteUrl)= _multimediaService.GetById(source.CoverId);

        return new MultimediaResponse(
            Id,
            ReadUrl,
            WriteUrl);
    }
}