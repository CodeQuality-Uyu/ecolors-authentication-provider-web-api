using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Extensions;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

[ApiController]
[Route("sessions")]
public class SessionController(
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper mapper,
    ISessionService sessionService)
    : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<SessionCreatedResponse> CreateAsync(CreateSessionCredentialsArgs request)
    {
        var session = await sessionService
            .CreateAndSaveAsync(request)
            .ConfigureAwait(false);

        return mapper.Map<SessionCreatedResponse>(session);
    }

    [HttpDelete]
    [SecureAuthorization]
    public async Task DeleteAsync()
    {
        var accountLogged = this.GetAccountLogged();

        await sessionService
            .DeleteAsync(accountLogged)
            .ConfigureAwait(false);
    }
}
