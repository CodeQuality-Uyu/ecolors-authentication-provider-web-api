using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Extensions;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.ApiElements.Filters.Authentications;
using CQ.Utility;
using Azure;
using CQ.ApiElements.Filters.ExceptionFilter;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

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

    [BearerAuthentication]
    [SecureAuthorization]
    [HttpPost("check")]
    public IActionResult Check(CheckRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        if (Guard.IsNullOrEmpty(request.Permission))
        {
            return NoContent();
        }

        var hasPermission = accountLogged.IsInRole(request.Permission);
        if (!hasPermission)
        {
            return new ObjectResult(new
            {
                code = "Forbidden",
                message = "Insufficient permissions",
                description = $"You don't have the permission {request.Permission} to access this request"
            })
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };
        }
        return NoContent();
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
