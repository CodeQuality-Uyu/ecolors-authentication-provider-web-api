using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.WebApi.Controllers.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

internal sealed class MeProfile
    : Profile
{
    public MeProfile()
    {
        CreateMap<AccountLogged, SessionCreatedResponse>()
            .ForMember(
            dest => dest.ProfilePicture,
            opt => opt.MapFrom<ProfilePictureResolver>())
            .ForMember(
            dest => dest.Token,
            opt => opt.MapFrom(
                src => $"Bearer {src.Token}"))
            .ForMember(
            dest => dest.Roles,
            opt => opt.MapFrom(
                src => src.Roles.ConvertAll(r => r.Name)))
            .ForMember(
            dest => dest.Permissions,
            opt => opt.MapFrom(
                src => src.Roles.SelectMany(r => r.Permissions).ToList().ConvertAll(p => p.Key)))
            ;
    }
}

internal sealed class ProfilePictureResolver(IBlobService blobService)
    : IValueResolver<AccountLogged, SessionCreatedResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        AccountLogged source,
        SessionCreatedResponse destination,
        BlobReadResponse? destMember,
        ResolutionContext context)
    {
        if (source.ProfilePictureKey == null)
        {
            return null;
        }

        return blobService.GetByKey(source.ProfilePictureKey);
    }
}
