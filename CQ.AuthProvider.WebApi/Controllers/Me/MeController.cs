using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Me.Models;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Filters;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

[ApiController]
[Route("me")]
[CQAuthentication]
public sealed class MeController(
    IAccountService accountService,
    IMapper mapper)
    : ControllerBase
{
    [HttpGet("me")]
    public AccountLoggedResponse GetMeAsync()
    {
        var accountLogged = this.GetAccountLogged();

        return mapper.Map<AccountLoggedResponse>(accountLogged);
    }

    [HttpPatch("me/password")]
    public async Task UpdatePasswordAsync(UpdatePasswordRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await accountService
            .UpdateAsync(
            args,
            accountLogged)
            .ConfigureAwait(false);
    }
}
