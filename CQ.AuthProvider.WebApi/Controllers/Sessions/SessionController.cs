using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Controllers.Sessions.Models;
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

[ApiController]
[Route("sessions")]
public class SessionController(
    IMapper mapper,
    ISessionService sessionService) : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<CreateSessionResponse> CreateAsync(CreateSessionCredentialsRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var createAuth = request.Map();

        var session = await sessionService
            .CreateAsync(createAuth)
            .ConfigureAwait(false);

        return mapper.Map<CreateSessionResponse>(session);
    }

    [HttpDelete]
    [CQAuthorization]
    [ValidateAccount]
    public async Task DeleteAsync()
    {
        var accountLogged = this.GetAccountLogged();

        await sessionService
            .DeleteAsync(accountLogged)
            .ConfigureAwait(false);
    }


    [HttpGet("{token}/validate")]
    [CQAuthorization]
    [ValidateClientSystem]
    public async Task<TokenValidationResponse> ValidateTokenAsync(string token)
    {
        var isValid = await sessionService
            .IsTokenValidAsync(token)
            .ConfigureAwait(false);

        return mapper.Map<TokenValidationResponse>(isValid);
    }
}
