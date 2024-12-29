using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations;

[ApiController]
[Route("invitations")]
public sealed class InvitationController(
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper,
    IInvitationService _invitationService)
    : ControllerBase
{
    [HttpPost]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task CreateAsync(CreateInvitationArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _invitationService
            .CreateAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task<Pagination<InvitationBasicInfoResponse>> GetAllAsync(
        [FromQuery] Guid? creatorId,
        [FromQuery] Guid? appId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var invitations = await _invitationService
            .GetAllAsync(
            creatorId,
            appId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<InvitationBasicInfoResponse>>(invitations);
    }

    [HttpPut("{id}/accept")]
    public async Task<CreateAccountResult> AcceptAsync(
        Guid id,
        AcceptInvitationArgs request)
    {
        var account = await _invitationService
        .AcceptByIdAsync(
            id,
            request)
            .ConfigureAwait(false);

        return account;
    }

    [HttpPut("{id}/declain")]
    public async Task DeclainAsync(
        Guid id,
        DeclainInvitationArgs request)
    {
        await _invitationService
            .DeclainByIdAsync(
            id,
            request)
            .ConfigureAwait(false);
    }
}
