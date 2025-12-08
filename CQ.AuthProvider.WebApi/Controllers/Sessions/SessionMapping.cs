using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

internal sealed class SessionMapping
    : Profile
{
    public SessionMapping()
    {
        #region Create
        CreateMap<Session, SessionCreatedResponse>()
            .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(
                src => src.Account.Id))
            .ForMember(
            dest => dest.ProfilePicture,
            opt => opt.MapFrom<ProfilePictureResolver>())
            .ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(
                src => src.Account.Email))
            .ForMember(
            dest => dest.FirstName,
            opt => opt.MapFrom(
                src => src.Account.FirstName))
            .ForMember(
            dest => dest.LastName,
            opt => opt.MapFrom(
                src => src.Account.LastName))
            .ForMember(
            dest => dest.FullName,
            opt => opt.MapFrom(
                src => src.Account.FullName))
            .ForMember(
            dest => dest.Token,
            opt => opt.MapFrom(
                src => $"Bearer {src.Token}"))
            .ForMember(
            dest => dest.Roles,
            opt => opt.MapFrom(
                src => src.Account.Roles.ConvertAll(r => r.Name)))
            .ForMember(
            dest => dest.Permissions,
            opt => opt.MapFrom(
                src => src.Account.Roles.SelectMany(r => r.Permissions).ToList().ConvertAll(p => p.Key)))
            .ForMember(
            dest => dest.AppLogged,
            opt => opt.MapFrom(
                src => src.App))
            .ForMember(
            dest => dest.Tenant,
            opt => opt.MapFrom(
                src => src.Account.Tenant))
            ;

        CreateMap<App, SessionAppLoggedResponse>();
        #endregion
    }
}

internal sealed class ProfilePictureResolver(IBlobService blobService)
    : IValueResolver<Session, SessionCreatedResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        Session source,
        SessionCreatedResponse destination,
        BlobReadResponse? destMember,
        ResolutionContext context)
    {
        if (source.Account.ProfilePictureKey == null)
        {
            return null;
        }

        return blobService.GetByKey(source.Account.ProfilePictureKey);
    }
}
