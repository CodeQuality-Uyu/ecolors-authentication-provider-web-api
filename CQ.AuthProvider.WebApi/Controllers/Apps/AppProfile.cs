using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
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

internal sealed class CoverMultimediaResolver(IBlobService _blobService)
    : IValueResolver<App, AppDetailInfoResponse, CoverMultimediaResponse>
{
    public CoverMultimediaResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        CoverMultimediaResponse destMember,
        ResolutionContext context)
    {
        var (Id, Key, ReadUrl) = _blobService.GetReadElementInApp(source, source.CoverId);

        BlobReadResponse? backgroundCover = null;
        if (Guard.IsNull(source.BackgroundCoverId))
        {
            var (backgroundCoverId, backgroundCoverKey, backgroundCoverReadUrl) = _blobService.GetReadElementInApp(source, source.BackgroundCoverId.Value);
            backgroundCover = new BlobReadResponse
            {
                Id = backgroundCoverId,
                Key = backgroundCoverKey,
                ReadUrl = backgroundCoverReadUrl,
            };
        }
        return new CoverMultimediaResponse
        {
            Id = Id,
            Key = Key,
            ReadUrl = ReadUrl,
            BackgroundColorHex = source.BackgroundCoverColorHex,
            BackgroundCover = backgroundCover
        };
    }
}