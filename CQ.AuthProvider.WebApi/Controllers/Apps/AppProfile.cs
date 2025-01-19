using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Multimedias;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Models;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

internal sealed class AppProfile
    : Profile
{
    public AppProfile()
    {
        #region Get all
        this.CreatePaginationMap<App, AppBasicInfoResponse>();
        #endregion

        #region Get by id
        CreateMap<App, AppDetailInfoResponse>()
            .ForMember(destination => destination.Cover,
            options => options.MapFrom<CoverMultimediaResolver>());

        #endregion
    }
}

internal sealed class CoverMultimediaResolver(IMultimediaService _multimediaService)
    : IValueResolver<App, AppDetailInfoResponse, CoverMultimediaResponse>
{
    public CoverMultimediaResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        CoverMultimediaResponse destMember,
        ResolutionContext context)
    {
        var (Id, ReadUrl, WriteUrl) = _multimediaService.GetById(source.CoverId);

        MultimediaResponse? backgroundCover = null;
        if (Guard.IsNull(source.BackgroundCoverId))
        {
            var (backgroundCoverId, backgroundCoverReadUrl, backgroundCoverWriteUrl) = _multimediaService.GetById(source.BackgroundCoverId.Value);
            backgroundCover = new MultimediaResponse
            {
                Id = backgroundCoverId,
                ReadUrl = backgroundCoverReadUrl,
                WriteUrl = backgroundCoverWriteUrl
            };
        }
        return new CoverMultimediaResponse
        {
            Id = Id,
            ReadUrl = ReadUrl,
            WriteUrl = WriteUrl,
            BackgroundColorHex = source.BackgroundCoverColorHex,
            BackgroundCover = backgroundCover
        };
    }
}