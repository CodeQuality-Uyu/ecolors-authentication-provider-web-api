using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.WebApi.Controllers.Tenants;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

internal sealed class SessionProfile
    : Profile
{
    public SessionProfile()
    {
        #region Create
        CreateMap<Session, SessionCreatedResponse>()
            .ConvertUsing((source, destination, options) => new SessionCreatedResponse(
                source.Account.Id,
                source.Account.ProfilePictureId,
                source.Account.Email,
                source.Account.FirstName,
                source.Account.LastName,
                source.Account.FullName,
                $"Bearer {source.Token}",
                source.Account.Roles.ConvertAll(r => r.Name),
                source
                .Account
                .Roles
                .SelectMany(r => r.Permissions)
                .Select(p => p.Key)
                .ToList(),
                options.Mapper.Map<SessionAppLoggedResponse>(source.App),
                options.Mapper.Map<TenantOfAccountBasicInfoResponse>(source.Account.Tenant)
            ));

        CreateMap<App, SessionAppLoggedResponse>();
        #endregion
    }
}
