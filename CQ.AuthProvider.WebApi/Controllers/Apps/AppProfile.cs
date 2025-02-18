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
    : IValueResolver<App, AppDetailInfoResponse, CoverBlobResponse>
{
    public CoverBlobResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        CoverBlobResponse destMember,
        ResolutionContext context)
    {
        var (Id, Key, ReadUrl) = _blobService.GetReadElementInApp(source, source.CoverId);

        BlobReadResponse? backgroundCover = null;
        if (source.BackgroundCoverId.HasValue)
        {
            var (backgroundCoverId, backgroundCoverKey, backgroundCoverReadUrl) = _blobService.GetReadElementInApp(source, source.BackgroundCoverId.Value);
            backgroundCover = new BlobReadResponse
            {
                Id = backgroundCoverId,
                Key = backgroundCoverKey,
                Url = backgroundCoverReadUrl,
            };
        }
        return new CoverBlobResponse
        {
            Id = Id,
            Key = Key,
            Url = ReadUrl,
            BackgroundColorHex = source.BackgroundCoverColorHex,
            BackgroundCover = backgroundCover
        };
    }
}