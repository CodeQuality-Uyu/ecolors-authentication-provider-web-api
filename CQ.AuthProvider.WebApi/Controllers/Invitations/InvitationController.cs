using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;
using CQ.AuthProvider.WebApi.Controllers.Invitations.Models;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Filters;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations;

[ApiController]
[Route("invitations")]
public sealed class InvitationController(
    IMapper mapper,
    IInvitationService invitationService)
    : ControllerBase
{
    [HttpPost]
    [CQAuthorization]
    public async Task CreateAsync(CreateInvitationRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await invitationService
            .CreateAsync(args)
            .ConfigureAwait(false);
    }

    [HttpGet]
    [CQAuthorization]
    public async Task<List<InvitationBasicInfoResponse>> GetAllAsync()
    {
        var accountLogged = this.GetAccountLogged();

        var invitations = await invitationService
            .GetAllAsync(accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<List<InvitationBasicInfoResponse>>(invitations);
    }

    [HttpPatch("{id}/accept")]
    public async Task AcceptAsync(string id)
    { 
    }

    [HttpPatch("{id}/accept-tenant")]
    public async Task AcceptTenantAsync(string id)
    {
    }

    [HttpPatch("{id}/declain")]
    public async Task DeclainAsync(string id)
    {
    }

    [HttpGet("{id}")]
    public async Task GetActiveByIdAsync(string id)
    {
    }
}
