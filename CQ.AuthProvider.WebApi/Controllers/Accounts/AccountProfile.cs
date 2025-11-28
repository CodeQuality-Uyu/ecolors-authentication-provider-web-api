using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Utils;
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

internal sealed class ProfilePictureResolver(IBlobService blobService)
    : IValueResolver<CreateAccountResult, SessionCreatedResponse, BlobReadResponse?>
{
    public BlobReadResponse? Resolve(
        CreateAccountResult source,
        SessionCreatedResponse destination,
        BlobReadResponse? destMember,
        ResolutionContext context)
    {
        if (source.ProfilePictureKey == null)
        {
            return null;
        }

        var blob = blobService.GetByKey(source.ProfilePictureKey);

        return blob;
    }
}

