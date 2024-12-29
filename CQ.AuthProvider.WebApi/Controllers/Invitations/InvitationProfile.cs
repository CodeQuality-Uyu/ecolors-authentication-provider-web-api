using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations;

internal sealed class InvitationProfile
    : Profile
{
    public InvitationProfile()
    {
        #region GetAll
        CreateMap<Invitation, InvitationBasicInfoResponse>()
            .ForMember(
            destination => destination.HasExpired,
            options => options.MapFrom(
                source => source.ExpiresAt <= DateTime.UtcNow));

        this.CreateOnlyPaginationMap<Invitation, InvitationBasicInfoResponse>();
        #endregion
    }
}
