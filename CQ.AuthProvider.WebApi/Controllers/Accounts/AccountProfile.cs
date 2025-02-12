using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
using CQ.AuthProvider.WebApi.Controllers.Sessions;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

internal sealed class AccountProfile
    : Profile
{
    public AccountProfile()
    {
        this.CreatePaginationMap<Account, AccountBasicInfoResponse>();

        #region Create
        CreateMap<CreateAccountResult, SessionCreatedResponse>()
            .ForMember(
            dest => dest.ProfilePicture,
            opt => opt.MapFrom<ProfilePictureResolver>())
            .ForMember(
            dest => dest.Token,
            opt => opt.MapFrom(
                src => $"Bearer {src.Token}"))
            ;
        #endregion

        #region Create credentials for
        CreateMap<Account, CreateCredentialsForResponse>();
        #endregion
    }
}

internal sealed class ProfilePictureResolver(IBlobService _blobService)
    : IValueResolver<CreateAccountResult, SessionCreatedResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        CreateAccountResult source,
        SessionCreatedResponse destination,
        BlobReadResponse destMember,
        ResolutionContext context)
    {
        if (source.ProfilePictureId == null)
        {
            return null;
        }

        var blob = _blobService.GetReadProfilePicture(
            source.ProfilePictureId.Value,
            source.Id,
            source.AppLogged.Name,
            source.Tenant.Name);

        return new BlobReadResponse
        {
            Id = blob.Id,
            Key = blob.Key,
            ReadUrl = blob.ReadUrl,
        };
    }
}

