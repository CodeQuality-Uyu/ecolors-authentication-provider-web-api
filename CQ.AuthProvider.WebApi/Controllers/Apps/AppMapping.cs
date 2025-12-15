using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Utils;
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
            .ForMember(destination => destination.Logo,
            options => options.MapFrom<LogoMultimediaResolver>())
            .ForMember(destination => destination.Background,
            options => options.MapFrom<BackgroundMultimediaResolver>());
        #endregion

        #region Create
        CreateMap<App, AppCreatedResponse>();
        #endregion
    }
}

internal sealed class LogoMultimediaResolver(IBlobService blobService)
    : IValueResolver<App, AppDetailInfoResponse, LogoResponse>
{
    public LogoResponse Resolve(
        App source,
        AppDetailInfoResponse destination,
        LogoResponse destMember,
        ResolutionContext context)
    {
        var color = blobService.GetByKey(source.Logo.ColorKey);
        var light = blobService.GetByKey(source.Logo.LightKey);
        var dark = blobService.GetByKey(source.Logo.DarkKey);

        return new LogoResponse
        {
            Color = color,
            Light = light,
            Dark = dark,
        };
    }
}

internal sealed class BackgroundMultimediaResolver(IBlobService blobService)
    : IValueResolver<App, AppDetailInfoResponse, BackgroundResponse?>
{
    public BackgroundResponse? Resolve(
        App source,
        AppDetailInfoResponse destination,
        BackgroundResponse? destMember,
        ResolutionContext context)
    {
        if(Guard.IsNull(source.Background))
        {
            return null!;
        }

        if (Guard.IsNullOrEmpty(source.Background.BackgroundKey))
        {
            return new BackgroundResponse
            {
                Colors = source.Background.Colors ?? [],
                Config = source.Background.Config,
            };
        }

        var background = blobService.GetByKey(source.Background.BackgroundKey!);

        return new BackgroundResponse
        {
            Image = background,
            Colors = source.Background.Colors ?? [],
            Config = source.Background.Config,
        };
    }
}