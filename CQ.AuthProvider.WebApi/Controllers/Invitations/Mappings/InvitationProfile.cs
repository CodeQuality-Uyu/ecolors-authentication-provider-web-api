using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Mappings;

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

        #region Accept
        CreateMap<CreateAccountResult, AccountCreatedResponse>();
        #endregion
    }
}
