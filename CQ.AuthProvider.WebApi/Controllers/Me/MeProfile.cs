using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Controllers.Sessions.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

internal sealed class MeProfile
    : Profile
{
    public MeProfile()
    {
        CreateMap<AccountLogged, SessionCreatedResponse>()
            .ConvertUsing((source, destination, options) => new SessionCreatedResponse(
                source.Id,
                source.ProfilePictureId,
                source.Email,
                source.FirstName,
                source.LastName,
                source.FullName,
                source.Token,
                source.Roles.ConvertAll(r => r.Name),
                source.PermissionsKeys
            ));
    }
}
