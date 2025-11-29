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

        CreateMap<CoverBackgroundColor, CoverBackgroundColorResponse>();
        #endregion

        #region Create
        CreateMap<App, AppCreatedResponse>();
        #endregion
    }
}

internal sealed class CoverMultimediaResolver(IBlobService blobService)
    : IValueResolver<App, AppDetailInfoResponse, CoverBlobResponse>
{
    public CoverBlobResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        CoverBlobResponse destMember,
        ResolutionContext context)
    {
        var blobCover = blobService.GetByKey(source.CoverKey);

        BlobReadResponse? backgroundCover = null;
        if (Guard.IsNotNullOrEmpty(source.BackgroundCoverKey))
        {
            backgroundCover = blobService.GetByKey(source.BackgroundCoverKey!);
        }
        return new CoverBlobResponse
        {
            Key = blobCover.Key,
            Url = blobCover.Url,
            BackgroundColor = context.Mapper.Map<CoverBackgroundColorResponse>(source.BackgroundColor),
            BackgroundCover = backgroundCover
        };
    }
}