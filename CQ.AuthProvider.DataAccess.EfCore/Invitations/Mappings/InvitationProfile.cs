using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Invitations;

namespace CQ.AuthProvider.DataAccess.EfCore.Invitations.Mappings;

internal sealed class InvitationProfile
    : Profile
{
    public InvitationProfile()
    {
        CreateMap<Invitation, InvitationEfCore>()
            .ConvertUsing((source, destination, context) => new InvitationEfCore(source));
    }
}
