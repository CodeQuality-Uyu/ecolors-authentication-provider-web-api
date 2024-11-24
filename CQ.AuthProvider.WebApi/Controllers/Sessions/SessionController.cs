using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Controllers.Sessions.Models;
using AutoMapper;
using CQ.Utility;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.ApiElements;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

[ApiController]
[Route("sessions")]
public class SessionController(
    IMapper mapper,
    ISessionService sessionService)
    : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<SessionCreatedResponse> CreateAsync(CreateSessionCredentialsRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var createAuth = request.Map();

        var session = await sessionService
            .CreateAndSaveAsync(createAuth)
            .ConfigureAwait(false);

        return mapper.Map<SessionCreatedResponse>(session);
    }

    [HttpDelete]
    [SecureAuthorization(ContextItem.AccountLogged)]
    public async Task DeleteAsync()
    {
        var accountLogged = this.GetAccountLogged();

        await sessionService
            .DeleteAsync(accountLogged)
            .ConfigureAwait(false);
    }
}
